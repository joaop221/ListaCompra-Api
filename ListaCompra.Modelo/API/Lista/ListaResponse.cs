using ListaCompra.Modelo.API.Grupo;

namespace ListaCompra.Modelo.API.Lista
{
    /// <summary>
    /// Response de lista
    /// </summary>
    public class ListaResponse
    {
        /// <summary>
        /// Id da lista
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Titulo da lista
        /// </summary>
        public string Titulo { get; set; }

        /// <summary>
        /// Grupo
        /// </summary>
        public GrupoResponse Grupo { get; set; }
    }
}