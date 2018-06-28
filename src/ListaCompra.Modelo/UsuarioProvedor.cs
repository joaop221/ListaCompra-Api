using FluentValidation.Attributes;
using ListaCompra.Modelo.Enumeradores;
using ListaCompra.Modelo.Validator;
using System;
using System.Security.Cryptography;
using System.Text;

namespace ListaCompra.Modelo
{
	/// <summary>
	/// Modelo de usuário provedor de identidade
	/// </summary>
	[Validator(typeof(UsuarioProvedorValidator))]
	public class UsuarioHelper : Base
	{
        /// <summary>
        /// Armazenha hash da string
        /// </summary>
        private string hash;

        /// <summary>
        /// Define qual o provedor de identidade do usuário
        /// </summary>
        public Provedor Provedor { get; set; }

        /// <summary>
        /// Email do usuário
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Hash do usuário retornada do provedor de identidade
        /// </summary>
        public string HashProvedor { get; set; }

        /// <summary>
        /// Senha do usuário
        /// Não estara preenchida sempre
        /// </summary>
        public string HashSenha
        {
            get => hash;
            set
            {
                try
                {
                    // Caso valor não esteja preenchido
                    if (!string.IsNullOrWhiteSpace(value))
                    {
                        // Instancia classe para extrair hash da string
                        using (var md5 = new MD5CryptoServiceProvider())
                        {
                            // Retorna array de bytes da hash extraida
                            var sha1data = md5.ComputeHash(Encoding.ASCII.GetBytes(value));

                            // Retorna hash a apartir do array de bytes
                            hash = Encoding.ASCII.GetString(sha1data);
                        }
                    }
                } catch (DecoderFallbackException)
                {
                    //TODO: Tratamento
                } catch (ArgumentException)
                {
                    //TODO: Tratamento
                } finally
                {
                    hash = string.Empty;
                }
            }
        }
    }
}