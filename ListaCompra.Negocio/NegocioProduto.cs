using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using ListaCompra.Modelo.API.Produto;
using ListaCompra.Modelo.Entidades;
using ListaCompra.Modelo.Interfaces;

namespace ListaCompra.Negocio
{
    public class NegocioProduto : INegocio
    {
        private readonly IRepositorio<Produto> repositorio;
        private readonly IMapper mapper;

        public NegocioProduto(IRepositorio<Produto> repositorio, IMapper mapper)
        {
            this.repositorio = repositorio;
            this.mapper = mapper;
        }

        /// <summary>
        /// Obter
        /// </summary>
        public async Task<ProdutoResponse> Obter(int id)
        {
            Produto entidade = await this.repositorio.ObterAsync(id);

            return this.mapper.Map<ProdutoResponse>(entidade);
        }

        /// <summary>
        /// Listar
        /// </summary>
        public async Task<List<ProdutoResponse>> Listar()
        {
            List<Produto> entidade = await this.repositorio.ConsultarAsync(x => true);

            return this.mapper.Map<List<ProdutoResponse>>(entidade);
        }

        /// <summary>
        /// Criar
        /// </summary>
        /// <returns>Resultado do check</returns>
        public async Task<ProdutoResponse> Criar(ProdutoRequest model)
        {
            Produto entidade = this.mapper.Map<Produto>(model);

            entidade = await this.repositorio.InserirAsync(entidade);

            return this.mapper.Map<ProdutoResponse>(entidade);
        }

        /// <summary>
        /// Atualizar
        /// </summary>
        public async Task Atualizar(int id, ProdutoRequest model)
        {
            Produto entidade = this.mapper.Map<Produto>(model);
            entidade.Id = id;

            await this.repositorio.AtualizarAsync(entidade);
        }

        /// <summary>
        /// Excluir
        /// </summary>
        public async Task Excluir(int id) => await this.repositorio.ExcluirAsync(x => x.Id == id);
    }
}