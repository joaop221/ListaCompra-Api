using FluentValidation;
using ListaCompra.Modelo.API.Grupo;

namespace ListaCompra.Modelo.API.Lista
{
    /// <summary>
    /// Reuqest para listas
    /// </summary>
    public class ListaComGrupoRequest
    {
        /// <summary>
        /// Titulo da lista
        /// </summary>
        public string Titulo { get; set; }

        /// <summary>
        /// Grupo
        /// </summary>
        public GrupoRequest Grupo { get; set; }
    }


    /// <summary>
    /// Validacao
    /// </summary>
    public class ListaComGrupoRequestValidacao : AbstractValidator<ListaComGrupoRequest>
    {
        /// <summary>
        /// Rergras
        /// </summary>
        public ListaComGrupoRequestValidacao()
        {
            RuleFor(x => x.Titulo)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(x => x.Grupo)
                .NotEmpty();
        }
    }
}