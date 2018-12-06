using FluentValidation;

namespace ListaCompra.Modelo.API.Grupo
{
    /// <summary>
    /// Request para atualizar categoria
    /// </summary>
    public class GrupoRequest
    {
        /// <summary>
        /// Nome do Grupo
        /// </summary>
        public string Nome { get; set; }
    }

    /// <summary>
    /// Validacao
    /// </summary>
    public class GrupoRequestValidacao : AbstractValidator<GrupoRequest>
    {
        /// <summary>
        /// Rergras
        /// </summary>
        public GrupoRequestValidacao()
        {
            RuleFor(x => x.Nome)
                .NotEmpty()
                .MaximumLength(150);
        }
    }
}