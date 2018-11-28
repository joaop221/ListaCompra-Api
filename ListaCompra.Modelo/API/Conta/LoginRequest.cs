using System.ComponentModel.DataAnnotations;
using FluentValidation;

namespace ListaCompra.Modelo.API.Conta
{
    /// <summary>
    /// Request de login
    /// </summary>
    public class LoginRequest
    {
        /// <summary>
        /// Nome do usuario
        /// </summary>
        public string NomeUsuario { get; set; }

        /// <summary>
        /// Senha
        /// </summary>
        public string Senha { get; set; }
    }

    /// <summary>
    /// Validacao
    /// </summary>
    public class LoginRequestValidacao : AbstractValidator<LoginRequest>
    {
        /// <summary>
        /// Rergras
        /// </summary>
        public LoginRequestValidacao()
        {
            RuleFor(x => x.NomeUsuario)
                .NotEmpty();

            RuleFor(x => x.Senha)
                .NotEmpty();
        }
    }
}