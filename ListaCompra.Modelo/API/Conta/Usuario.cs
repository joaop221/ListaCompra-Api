using FluentValidation;
using ListaCompra.Modelo.API.Grupo;

namespace ListaCompra.Modelo.API.Conta
{
    /// <summary>
    /// Usuario
    /// </summary>
    public class Usuario
    {
        /// <summary>
        /// Nome do Usuario
        /// </summary>
        public string Nome { get; set; }

        /// <summary>
        /// Email do usuario
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Permissao
        /// </summary>
        public Permissao Permissao { get; set; }
    }

    /// <summary>
    /// Validacao
    /// </summary>
    public class UsuarioValidacao : AbstractValidator<Usuario>
    {
        /// <summary>
        /// Rergras
        /// </summary>
        public UsuarioValidacao()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(x => x.Nome)
                .NotEmpty();

            RuleFor(x => x.Permissao)
                .IsInEnum();
        }
    }
}
