using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using ListaCompra.Infraestrutura.Tratamento;
using ListaCompra.Modelo;
using ListaCompra.Modelo.API.Account;
using ListaCompra.Modelo.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace ListaCompra.Negocio
{
    public class NegocioConta : INegocio
    {
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly UserManager<IdentityUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IConfiguration configuration;
        private readonly IHttpContextAccessor httpContextAccessor;

        public NegocioConta(UserManager<IdentityUser> userManager,
                                    SignInManager<IdentityUser> signInManager,
                                    IConfiguration configuration,
                                    RoleManager<IdentityRole> roleManager,
                                    IHttpContextAccessor httpContextAccessor)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.configuration = configuration;
            this.roleManager = roleManager;
            this.httpContextAccessor = httpContextAccessor;
        }

        public async Task<LoginResponse> Login(LoginRequest model)
        {
            SignInResult result =
                await this.signInManager.PasswordSignInAsync(model.NomeUsuario, model.Senha, false, false);

            if (result.Succeeded)
            {
                IdentityUser appUser = this.userManager.Users.SingleOrDefault(r => r.UserName == model.NomeUsuario);
                IList<string> teste = await this.userManager.GetRolesAsync(appUser);

                var token = await GeraTokenJwt(model.NomeUsuario, appUser);

                return new LoginResponse(token);
            }

            throw new FalhaLoginExcecao("Login inválido");
        }

        public async Task<RegistroResponse> Registro(RegistroRequest model)
        {
            // Criado para apenas commitar as mudanças caso todos os passos do login funcione
            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var user = new IdentityUser
                {
                    UserName = model.Nome,
                    Email = model.Email
                };

                IdentityResult userResult = await this.userManager.CreateAsync(user, model.Senha);

                if (!userResult.Succeeded)
                    throw new RegistroExcecao(userResult.Errors);

                // Adiciona as funcoes do usuario
                await AdicionaFuncoes(model.Funcoes, user);

                await this.signInManager.SignInAsync(user, false);

                var token = await GeraTokenJwt(model.Email, user);

                scope.Complete();

                return new RegistroResponse(token);
            }
        }

        public async Task<RetornoErro> ExcluirConta(LoginRequest model)
        {
            var sbErros = new StringBuilder();

            // Criado para apenas commitar as mudanças caso todos os passos do login funcione
            IdentityUser user = await this.userManager.FindByNameAsync(model.NomeUsuario);
            if (user == null)
                throw new ApiExcecao(422, "Este usuario não existe!");

            SignInResult result =
                await this.signInManager.CheckPasswordSignInAsync(user, model.Senha, false);

            if (!result.Succeeded)
                throw new FalhaLoginExcecao("Login inválido");

            IdentityResult userResult = await this.userManager.DeleteAsync(user);
            if (!userResult.Succeeded)
            {
                userResult.Errors.ToList().ForEach(x => sbErros.AppendLine(x.Description));
                throw new NegocioExcecao(sbErros.ToString());
            }

            return new RetornoErro("Usuario excluido com sucesso!", 200, ToString());
        }

        private async Task AdicionaFuncoes(List<string> funcoes, IdentityUser user)
        {
            if (!funcoes.Contains("Comum"))
                funcoes.Add("Comum");

            IdentityResult roleResult = await this.userManager.AddToRolesAsync(user, funcoes);
            if (!roleResult.Succeeded)
                throw new RegistroExcecao(roleResult.Errors);
        }

        private async Task<string> GeraTokenJwt(string email, IdentityUser usuario)
        {
            List<Claim> claims = await RecuperaClaims(usuario);

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.configuration["ChaveJwt"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            DateTime expires = DateTime.Now.AddDays(Convert.ToDouble(this.configuration["DiasExpiracaoJwt"]));

            var token = new JwtSecurityToken(
                this.configuration["JwtIssuer"],
                this.configuration["JwtIssuer"],
                claims,
                expires: expires,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private async Task<List<Claim>> RecuperaClaims(IdentityUser usuario)
        {
            var options = new IdentityOptions();
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, usuario.UserName),
                new Claim(JwtRegisteredClaimNames.Jti,  Guid.NewGuid().ToString()),
                new Claim(options.ClaimsIdentity.UserIdClaimType, usuario.Id.ToString()),
                new Claim(options.ClaimsIdentity.UserNameClaimType, usuario.UserName)
            };

            // Funcoes do usuario
            IList<string> userRoles = await this.userManager.GetRolesAsync(usuario);

            // Tranforma as funcoes em claims
            foreach (var userRole in userRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            return claims;
        }
    }
}