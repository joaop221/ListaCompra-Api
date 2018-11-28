using FluentValidation;

namespace ListaCompra.Modelo.API.Lista
{
    public class CriaListaRequest
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
    public class CriaListaRequestValidacao : AbstractValidator<CriaListaRequest>
    {
        /// <summary>
        /// Rergras
        /// </summary>
        public CriaListaRequestValidacao()
        {
            RuleFor(x => x.Titulo)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(x => x.GrupoId)
                .NotEmpty();
        }
    }
}