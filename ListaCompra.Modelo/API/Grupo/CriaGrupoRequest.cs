using FluentValidation;

namespace ListaCompra.Modelo.API.Grupo
{
    /// <summary>
    /// Request para atualizar categoria
    /// </summary>
    public class CriaGrupoRequest
    {
        /// <summary>
        /// Nome do Grupo
        /// </summary>
        public string Nome { get; set; }
    }

    /// <summary>
    /// Validacao
    /// </summary>
    public class CriaGrupoRequestValidacao : AbstractValidator<CriaGrupoRequest>
    {
        /// <summary>
        /// Rergras
        /// </summary>
        public CriaGrupoRequestValidacao()
        {
            RuleFor(x => x.Nome)
                .MaximumLength(150);
        }
    }
}