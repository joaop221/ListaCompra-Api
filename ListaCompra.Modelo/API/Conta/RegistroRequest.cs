using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using FluentValidation;

namespace ListaCompra.Modelo.API.Conta
{
    /// <summary>
    /// Request de registro
    /// </summary>
    public class RegistroRequest
    {
        /// <summary>
        /// Nome do usuario
        /// </summary>
        public string Nome { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Senha
        /// </summary>
        public string Senha { get; set; }

        /// <summary>
        /// Funcoes
        /// </summary>
        public List<string> Funcoes { get; set; }
    }

    /// <summary>
    /// Validacao
    /// </summary>
    public class RegistroRequestValidacao : AbstractValidator<RegistroRequest>
    {
        /// <summary>
        /// Rergras
        /// </summary>
        public RegistroRequestValidacao()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(x => x.Nome)
                .NotEmpty();

            RuleFor(x => x.Senha)
                .NotEmpty();
        }
    }
}