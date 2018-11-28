namespace ListaCompra.Modelo.API.Categoria
{
    /// <summary>
    /// Response de categoria
    /// </summary>
    public class CategoriaResponse
    {
        /// <summary>
        /// ID da categoria
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Nome da categoria
        /// </summary>
        public string NomeCategoria { get; set; }

        /// <summary>
        /// Decricao da categoria
        /// </summary>
        public string Descricao { get; set; }
    }
}