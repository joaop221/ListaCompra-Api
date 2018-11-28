using System;
using System.Threading.Tasks;
using AutoMapper;
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
        public async Task<object> Obter(int id)
            => throw new NotImplementedException();

        /// <summary>
        /// Listar
        /// </summary>
        public async Task<object> Listar(object model)
            => throw new NotImplementedException();

        /// <summary>
        /// Criar
        /// </summary>
        /// <returns>Resultado do check</returns>
        public async Task<object> Criar(object model)
            => throw new NotImplementedException();

        /// <summary>
        /// Atualizar
        /// </summary>
        public async Task<object> Atualizar(int id, object model)
            => throw new NotImplementedException();

        /// <summary>
        /// Excluir
        /// </summary>
        public async Task<object> Excluir(int id)
            => throw new NotImplementedException();
    }
}