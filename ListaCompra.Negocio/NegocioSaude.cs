using System.Threading.Tasks;
using ListaCompra.Modelo.Interfaces;

namespace ListaCompra.Negocio
{
    public class NegocioSaude : INegocio
    {
        private readonly IRepositorioSaude _repositorio;

        public NegocioSaude(IRepositorioSaude repositorio)
        {
            this._repositorio = repositorio;
        }

        /// <summary>
        /// Checa todas as dependências que o serviço usa
        /// </summary>
        public async Task Check() => await CheckDB();

        private async Task CheckDB() => await this._repositorio.Check();
    }
}