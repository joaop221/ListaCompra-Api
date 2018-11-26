using System.Threading.Tasks;

namespace ListaCompra.Modelo.Interfaces
{
    /// <summary>
    /// Interface para o repositorio de saude
    /// </summary>
    public interface IRepositorioSaude : IRepositorio
    {
        /// <summary>
        /// Verifica se a conexão com o banco está funcionando
        /// </summary>
        /// <returns></returns>
        Task Check();
    }
}