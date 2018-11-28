using FluentValidation;

namespace ListaCompra.Modelo.API.Categoria
{
    /// <summary>
    /// Request para atualizar categoria
    /// </summary>
    public class CriaCategoriaRequest
    {
        /// <summary>
        /// Nome da categoria
        /// </summary>
        public string NomeCategoria { get; set; }

        /// <summary>
        /// Decricao da categoria
        /// </summary>
        public string Descricao { get; set; }
    }

    /// <summary>
    /// Validacao
    /// </summary>
    public class CriaCategoriaRequestValidacao : AbstractValidator<CriaCategoriaRequest>
    {
        /// <summary>
        /// Rergras
        /// </summary>
        public CriaCategoriaRequestValidacao()
        {
            RuleFor(x => x.NomeCategoria)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(x => x.Descricao)
                .MaximumLength(300);
        }
    }
}