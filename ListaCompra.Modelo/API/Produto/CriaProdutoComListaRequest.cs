namespace ListaCompra.Modelo.API.Produto
{
    /// <summary>
    /// Usado para criar um produto e atrelar ele à uma lista
    /// </summary>
    public class CriaProdutoComListaRequest
    {
        /// <summary>
        /// Id da Lista
        /// </summary>
        public int ListaId { get; set; }

        /// <summary>
        /// Produto
        /// </summary>
        public ProdutoRequest Produto { get; set; }
    }
}