using System.Collections.Generic;
using System.Threading.Tasks;
using System.Transactions;
using AutoMapper;
using ListaCompra.Modelo.API.Produto;
using ListaCompra.Modelo.Base;
using ListaCompra.Modelo.Entidades;

namespace ListaCompra.Negocio
{
    public class NegocioProduto : INegocio
    {
        private readonly IRepositorio<Produto> repositorio;
        private readonly IRepositorio<ProdutoLista> repositorioProdutoLista;

        private readonly IMapper mapper;

        public NegocioProduto(IRepositorio<Produto> repositorio,
                                IRepositorio<ProdutoLista> repositorioProdutoLista,
                                IMapper mapper)
        {
            this.repositorio = repositorio;
            this.repositorioProdutoLista = repositorioProdutoLista;
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
            List<Produto> entidade = await this.repositorio.ConsultarAsync(x => x.Excluido == false);

            return this.mapper.Map<List<ProdutoResponse>>(entidade);
        }

        /// <summary>
        /// Listar
        /// </summary>
        public async Task<List<ProdutoResponse>> Listar(string termo)
        {
            List<Produto> entidade = await this.repositorio.ConsultarAsync(x => x.Nome.Contains(termo) && x.Excluido == false);

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
        /// Criar
        /// </summary>
        /// <returns>Resultado do check</returns>
        public async Task<ProdutoResponse> Criar(ProdutoRequest model, int listaId)
        {
            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                // Cria o produto
                ProdutoResponse entidade = await Criar(model);

                var produtoLista = new ProdutoLista()
                {
                    ListaId = listaId,
                    ProdutoId = entidade.Id
                };

                // Atrela a lista com o produto
                await this.repositorioProdutoLista.InserirAsync(produtoLista);

                scope.Complete();
                return this.mapper.Map<ProdutoResponse>(entidade);
            }
        }

        /// <summary>
        /// Criar
        /// </summary>
        /// <returns>Resultado do check</returns>
        public async Task<ProdutoResponse> Criar(CriaProdutoComListaRequest produtoComLista)
            => await Criar(produtoComLista.Produto, produtoComLista.ListaId);

        /// <summary>
        /// Criar
        /// </summary>
        /// <returns>Resultado do check</returns>
        public async Task<List<ProdutoResponse>> Criar(CriaListaProdutoComListaRequest listaProdutoComLista)
        {
            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var listaResponse = new List<ProdutoResponse>();

                foreach (ProdutoRequest produto in listaProdutoComLista.Produtos)
                {
                    listaResponse.Add(await Criar(produto, listaProdutoComLista.ListaId));
                }

                scope.Complete();
                return this.mapper.Map<List<ProdutoResponse>>(listaResponse);
            }
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