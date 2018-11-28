using FluentValidation;

namespace ListaCompra.Modelo.API.Grupo
{
    /// <summary>
    /// Request para atualizar atualiza
    /// </summary>
    public class AtualizaGrupoRequest
    {
        /// <summary>
        /// Nome do Grupo
        /// </summary>
        public string Nome { get; set; }
    }

    /// <summary>
    /// Validacao
    /// </summary>
    public class AtualizaGrupoRequestValidacao : AbstractValidator<AtualizaGrupoRequest>
    {
        /// <summary>
        /// Rergras
        /// </summary>
        public AtualizaGrupoRequestValidacao()
        {
            RuleFor(x => x.Nome)
                .MaximumLength(150);
        }
    }
}