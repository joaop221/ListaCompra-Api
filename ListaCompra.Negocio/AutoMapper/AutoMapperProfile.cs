using AutoMapper;
using ListaCompra.Modelo.API.Categoria;
using ListaCompra.Modelo.API.Grupo;
using ListaCompra.Modelo.API.Lista;
using ListaCompra.Modelo.API.Produto;
using ListaCompra.Modelo.Entidades;

namespace ListaCompra.Negocio
{
    public class AutoMapperProfile : Profile
    {
        /// <summary>
        /// Mapeamentos
        /// </summary>
        public AutoMapperProfile()
        {
            #region [ Categoria ]

            CreateMap<Categoria, CategoriaRequest>()
                .ForMember(x => x.Descricao, d => d.MapFrom(i => i.Descricao))
                .ForMember(x => x.NomeCategoria, d => d.MapFrom(i => i.Nome))
                .ReverseMap();

            CreateMap<Categoria, CategoriaResponse>()
                .ForMember(x => x.Descricao, d => d.MapFrom(i => i.Descricao))
                .ForMember(x => x.NomeCategoria, d => d.MapFrom(i => i.Nome))
                .ForMember(x => x.Id, d => d.MapFrom(i => i.Id))
                .ReverseMap();

            #endregion [ Categoria ]

            #region [ Grupo ]

            CreateMap<Grupo, GrupoRequest>()
                .ForMember(x => x.Nome, d => d.MapFrom(i => i.Nome))
                .ReverseMap();

            CreateMap<Grupo, GrupoResponse>()
                .ForMember(x => x.Nome, d => d.MapFrom(i => i.Nome))
                .ForMember(x => x.Id, d => d.MapFrom(i => i.Id))
                .ReverseMap();

            #endregion [ Grupo ]

            #region [ Lista ]

            CreateMap<Lista, ListaRequest>()
                .ForMember(x => x.GrupoId, d => d.MapFrom(i => i.GrupoId))
                .ForMember(x => x.Titulo, d => d.MapFrom(i => i.Titulo))
                .ReverseMap();

            CreateMap<Lista, ListaResponse>()
                .ForMember(x => x.GrupoId, d => d.MapFrom(i => i.GrupoId))
                .ForMember(x => x.Titulo, d => d.MapFrom(i => i.Titulo))
                .ReverseMap();

            #endregion [ Lista ]

            #region [ Produto ]

            CreateMap<Produto, ProdutoRequest>()
                .ForMember(x => x.Descricao, d => d.MapFrom(i => i.Descricao))
                .ForMember(x => x.Nome, d => d.MapFrom(i => i.Nome))
                .ForMember(x => x.Quantidade, d => d.MapFrom(i => i.Quantidade))
                .ForMember(x => x.CategoriaId, d => d.MapFrom(i => i.CategoriaId))
                .ForMember(x => x.Valor, d => d.MapFrom(i => i.Valor))
                .ReverseMap();

            CreateMap<Produto, ProdutoResponse>()
                .ForMember(x => x.Descricao, d => d.MapFrom(i => i.Descricao))
                .ForMember(x => x.Nome, d => d.MapFrom(i => i.Nome))
                .ForMember(x => x.Quantidade, d => d.MapFrom(i => i.Quantidade))
                .ForMember(x => x.CategoriaId, d => d.MapFrom(i => i.CategoriaId))
                .ForMember(x => x.Id, d => d.MapFrom(i => i.Id))
                .ForMember(x => x.Valor, d => d.MapFrom(i => i.Valor))
                .ReverseMap();

            #endregion [ Produto ]
        }
    }
}