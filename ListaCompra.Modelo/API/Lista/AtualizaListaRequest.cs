using FluentValidation;

namespace ListaCompra.Modelo.API.Lista
{
    /// <summary>
    /// Request para atualizar lista
    /// </summary>
    public class AtualizaListaRequest
    {
        /// <summary>
        /// Titulo da lista
        /// </summary>
        public string Titulo { get; set; }

        /// <summary>
        /// Id do grupo
        /// </summary>
        public int GrupoId { get; set; }
    }

    /// <summary>
    /// Validacao
    /// </summary>
    public class AtualizaListaRequestValidacao : AbstractValidator<AtualizaListaRequest>
    {
        /// <summary>
        /// Rergras
        /// </summary>
        public AtualizaListaRequestValidacao()
        {
            RuleFor(x => x.Titulo)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(x => x.GrupoId)
                .NotEmpty();
        }
    }
}