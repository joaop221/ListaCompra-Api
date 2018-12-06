using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using ListaCompra.Modelo.API.Lista;
using ListaCompra.Modelo.Base;
using ListaCompra.Modelo.Entidades;

namespace ListaCompra.Negocio
{
    public class NegocioLista : INegocio
    {
        private readonly IRepositorio<Lista> repositorio;
        private readonly IMapper mapper;

        public NegocioLista(IRepositorio<Lista> repositorio, IMapper mapper)
        {
            this.repositorio = repositorio;
            this.mapper = mapper;
        }

        /// <summary>
        /// Obter
        /// </summary>
        public async Task<ListaResponse> Obter(int id)
        {
            Lista entidade = await this.repositorio.ObterAsync(id);

            return this.mapper.Map<ListaResponse>(entidade);
        }

        /// <summary>
        /// Listar
        /// </summary>
        public async Task<List<ListaResponse>> Listar()
        {
            List<Lista> entidade = await this.repositorio.ConsultarAsync(x => x.Excluido == false);

            return this.mapper.Map<List<ListaResponse>>(entidade);
        }

        /// <summary>
        /// Criar
        /// </summary>
        /// <returns>Resultado do check</returns>
        public async Task<ListaResponse> Criar(ListaRequest model)
        {
            Lista entidade = this.mapper.Map<Lista>(model);

            entidade = await this.repositorio.InserirAsync(entidade);

            return this.mapper.Map<ListaResponse>(entidade);
        }

        /// <summary>
        /// Atualizar
        /// </summary>
        public async Task Atualizar(int id, ListaRequest model)
        {
            Lista entidade = this.mapper.Map<Lista>(model);
            entidade.Id = id;

            await this.repositorio.AtualizarAsync(entidade);
        }

        /// <summary>
        /// Excluir
        /// </summary>
        public async Task Excluir(int id) => await this.repositorio.ExcluirAsync(x => x.Id == id);
    }
}