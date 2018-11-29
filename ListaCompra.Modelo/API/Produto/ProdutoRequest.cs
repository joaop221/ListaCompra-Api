using FluentValidation;

namespace ListaCompra.Modelo.API.Produto
{
    /// <summary>
    /// Request para criar produto
    /// </summary>
    public class ProdutoRequest
    {
        /// <summary>
        /// Nome do produtos
        /// </summary>
        public string Nome { get; set; }

        /// <summary>
        /// Descicao do prduto
        /// </summary>
        public string Descricao { get; set; }

        /// <summary>
        /// Quantidade do produto
        /// </summary>
        public int Quantidade { get; set; }

        /// <summary>
        /// Valor do produtos(unicade)
        /// </summary>
        public double Valor { get; set; }

        /// <summary>
        /// Id da categoria
        /// </summary>
        public int CategoriaId { get; set; }
    }

    /// <summary>
    /// Validacao
    /// </summary>
    public class ProdutoRequestValidacao : AbstractValidator<ProdutoRequest>
    {
        /// <summary>
        /// Rergras
        /// </summary>
        public ProdutoRequestValidacao()
        {
            RuleFor(x => x.Nome)
                .NotEmpty()
                .MaximumLength(150);

            RuleFor(x => x.Descricao)
                .MaximumLength(300);

            RuleFor(x => x.Quantidade)
                .NotEmpty();

            RuleFor(x => x.Valor)
                .NotEmpty();

            RuleFor(x => x.CategoriaId)
                .NotEmpty();
        }
    }
}