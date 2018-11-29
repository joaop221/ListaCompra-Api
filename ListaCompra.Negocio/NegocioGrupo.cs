using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using ListaCompra.Modelo.API.Grupo;
using ListaCompra.Modelo.Entidades;
using ListaCompra.Modelo.Interfaces;

namespace ListaCompra.Negocio
{
    public class NegocioGrupo : INegocio
    {
        private readonly IRepositorio<Grupo> repositorio;
        private readonly IMapper mapper;

        public NegocioGrupo(IRepositorio<Grupo> repositorio, IMapper mapper)
        {
            this.repositorio = repositorio;
            this.mapper = mapper;
        }

        /// <summary>
        /// Obter
        /// </summary>
        public async Task<GrupoResponse> Obter(int id)
        {
            Grupo entidade = await this.repositorio.ObterAsync(id);

            return this.mapper.Map<GrupoResponse>(entidade);
        }
        /// <summary>
        /// Listar
        /// </summary>
        public async Task<List<GrupoResponse>> Listar()
        {
            List<Grupo> entidade = await this.repositorio.ConsultarAsync(x => true);

            return this.mapper.Map<List<GrupoResponse>>(entidade);
        }
        /// <summary>
        /// Criar
        /// </summary>
        /// <returns>Resultado do check</returns>
        public async Task<GrupoResponse> Criar(GrupoRequest model)
        {
            Grupo entidade = this.mapper.Map<Grupo>(model);

            entidade = await this.repositorio.InserirAsync(entidade);

            return this.mapper.Map<GrupoResponse>(entidade);
        }
        /// <summary>
        /// Atualizar
        /// </summary>
        public async Task Atualizar(int id, GrupoRequest model)
        {
            Grupo entidade = this.mapper.Map<Grupo>(model);
            entidade.Id = id;

            await this.repositorio.AtualizarAsync(entidade);
        }
        /// <summary>
        /// Excluir
        /// </summary>
        public async Task Excluir(int id) => await this.repositorio.ExcluirAsync(x => x.Id == id);
    }
}