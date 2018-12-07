using FluentValidation;
using ListaCompra.Modelo.API.Categoria;

namespace ListaCompra.Modelo.API.Produto
{
    /// <summary>
    /// Request para criar produto
    /// </summary>
    public class ProdutoRequest
    {
        /// <summary>
        /// Nulo caso seja um novo produto
        /// </summary>
        public int? Id { get; set; }

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
        /// Id da categoria caso ela exista
        /// </summary>
        public int? CategoriaId { get; set; }

        /// <summary>
        /// Categoria caso ela ainda n exista
        /// </summary>
        public CategoriaRequest Categoria { get; set; }
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

            When(prod => prod.Id == null, () =>
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

                When(prod => prod.CategoriaId == null, () =>
                {
                    RuleFor(x => x.Categoria)
                        .NotEmpty();
                });
            });
        }
    }
}