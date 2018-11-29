using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using ListaCompra.Modelo.API.Categoria;
using ListaCompra.Modelo.Entidades;
using ListaCompra.Modelo.Interfaces;

namespace ListaCompra.Negocio
{
    public class NegocioCategoria : INegocio
    {
        private readonly IRepositorio<Categoria> repositorio;
        private readonly IMapper mapper;

        public NegocioCategoria(IRepositorio<Categoria> repositorio, IMapper mapper)
        {
            this.repositorio = repositorio;
            this.mapper = mapper;
        }

        /// <summary>
        /// Obter
        /// </summary>
        public async Task<CategoriaResponse> Obter(int id)
        {
            Categoria entidade = await this.repositorio.ObterAsync(id);

            return this.mapper.Map<CategoriaResponse>(entidade);
        }

        /// <summary>
        /// Listar
        /// </summary>
        public async Task<List<CategoriaResponse>> Listar()
        {
            List<Categoria> entidade = await this.repositorio.ConsultarAsync(x => true);

            return this.mapper.Map<List<CategoriaResponse>>(entidade);
        }

        /// <summary>
        /// Criar
        /// </summary>
        /// <returns>Resultado do check</returns>
        public async Task<CategoriaResponse> Criar(CategoriaRequest model)
        {
            Categoria entidade = this.mapper.Map<Categoria>(model);

            entidade = await this.repositorio.InserirAsync(entidade);

            return this.mapper.Map<CategoriaResponse>(entidade);
        }

        /// <summary>
        /// Atualizar
        /// </summary>
        public async Task Atualizar(int id, CategoriaRequest model)
        {
            Categoria entidade = this.mapper.Map<Categoria>(model);
            await this.repositorio.AtualizarAsync(entidade);
        }

        /// <summary>
        /// Excluir
        /// </summary>
        public async Task Excluir(int id) => await this.repositorio.ExcluirAsync(x => x.Id == id);
    }
}