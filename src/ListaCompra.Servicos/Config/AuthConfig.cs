using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListaCompra.Servicos.Config
{
	/// <summary>
	/// 
	/// </summary>
	public static class AuthConfig
	{
		/// <summary>
		/// Define configurações de autenticação
		/// </summary>
		/// <param name="services">servicos da aplicação</param>
		/// <param name="signingKey">chave do token</param>
		public static void AddAuth(this IServiceCollection services, string signingKey)
		{
			services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
			{
				options.TokenValidationParameters = new TokenValidationParameters
				{
					// Clock skew compensates for server time drift.
					// We recommend 5 minutes or less:
					ClockSkew = TimeSpan.FromMinutes(30),
					// Specify the key used to sign the token:
					IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(signingKey)),
					RequireSignedTokens = true,
					// Ensure the token hasn't expired:
					RequireExpirationTime = true,
					ValidateLifetime = true,
					// Ensure the token audience matches our audience value (default true):
					ValidateAudience = true,
					ValidAudience = "api://default",
					// Ensure the token was issued by a trusted authorization server (default true):
					ValidateIssuer = true,
					ValidIssuer = "https://nate-example.oktapreview.com/oauth2/default"
				};
			});
		}
	}
}
