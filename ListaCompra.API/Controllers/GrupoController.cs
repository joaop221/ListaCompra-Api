using System.Collections.Generic;
using System.Threading.Tasks;
using ListaCompra.Modelo.API.Conta;
using ListaCompra.Modelo.API.Grupo;
using ListaCompra.Negocio;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace ListaCompra.API.Controllers
{
    /// <summary>
    /// Grupo
    /// </summary>
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class GrupoController : ControllerBase
    {
        private readonly NegocioGrupo negocioGrupo;

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="negocioGrupo"></param>
        public GrupoController(NegocioGrupo negocioGrupo)
        {
            this.negocioGrupo = negocioGrupo;
        }

        /// <summary>
        /// Obter grupo
        /// </summary>
        [SwaggerResponse(200, "Sucesso", typeof(GrupoResponse))]
        [SwaggerResponse(503, Description = "API ou algum recurso que ela depende está fora do ar")]
        [SwaggerResponse(500, Description = "Erro interno desconhecido")]
        [HttpGet("{id}")]
        public async Task<GrupoComUsuariosResponse> Obter([FromRoute] int id)
            => await this.negocioGrupo.Obter(id);

        /// <summary>
        /// Listar todos os grupos
        /// </summary>
        [SwaggerResponse(200, "Sucesso", typeof(GrupoResponse[]))]
        [SwaggerResponse(503, Description = "API ou algum recurso que ela depende está fora do ar")]
        [SwaggerResponse(500, Description = "Erro interno desconhecido")]
        [HttpPost("Listar")]
        public async Task<List<GrupoResponse>> Listar()
            => await this.negocioGrupo.Listar();

        /// <summary>
        /// Lista grupos do usuario logado
        /// </summary>
        [SwaggerResponse(200, "Sucesso", typeof(GrupoResponse[]))]
        [SwaggerResponse(503, Description = "API ou algum recurso que ela depende está fora do ar")]
        [SwaggerResponse(500, Description = "Erro interno desconhecido")]
        [HttpPost("Listar/Meu")]
        public async Task<List<GrupoResponse>> ListarMeusGrupos()
            => await this.negocioGrupo.ListarGrupoUsuarioAtual();

        /// <summary>
        /// Criar novo grupo
        /// </summary>
        /// <returns>Resultado do check</returns>
        [SwaggerResponse(200, "Sucesso", typeof(GrupoResponse))]
        [SwaggerResponse(503, Description = "API ou algum recurso que ela depende está fora do ar")]
        [SwaggerResponse(500, Description = "Erro interno desconhecido")]
        [HttpPost]
        public async Task<GrupoResponse> Criar([FromBody] GrupoRequest model)
            => await this.negocioGrupo.Criar(model);

        /// <summary>
        /// Criar grupo com usuarios
        /// </summary>
        /// <returns>Resultado do check</returns>
        [SwaggerResponse(200, "Sucesso", typeof(GrupoResponse))]
        [SwaggerResponse(503, Description = "API ou algum recurso que ela depende está fora do ar")]
        [SwaggerResponse(500, Description = "Erro interno desconhecido")]
        [HttpPost("Usuarios")]
        public async Task<GrupoResponse> CriarComUsuarios([FromBody] GrupoComUsuariosRequest model)
            => await this.negocioGrupo.CriarComUsuarios(model);

        /// <summary>
        /// Adiciona usuarios em um grupo
        /// </summary>
        /// <returns>Resultado do check</returns>
        [SwaggerResponse(200, "Sucesso")]
        [SwaggerResponse(503, Description = "API ou algum recurso que ela depende está fora do ar")]
        [SwaggerResponse(500, Description = "Erro interno desconhecido")]
        [HttpPost("{grupoId}/Usuario/Novo")]
        public async Task AdicionarUsuario(int grupoId, [FromBody] List<Usuario> usuario)
            => await this.negocioGrupo.AdicionarUsuario(grupoId, usuario);

        /// <summary>
        /// Altera permissao do usuario em um grupo
        /// </summary>
        /// <returns>Resultado do check</returns>
        [SwaggerResponse(200, "Sucesso")]
        [SwaggerResponse(503, Description = "API ou algum recurso que ela depende está fora do ar")]
        [SwaggerResponse(500, Description = "Erro interno desconhecido")]
        [HttpPut("{grupoId}/Permissao")]
        public async Task AlterarPermissao(int grupoId, [FromBody] Usuario usuario)
            => await this.negocioGrupo.AlterarPermissao(grupoId, usuario);

        /// <summary>
        /// Atualiza grupo
        /// </summary>
        [SwaggerResponse(200, "Sucesso")]
        [SwaggerResponse(503, Description = "API ou algum recurso que ela depende está fora do ar")]
        [SwaggerResponse(500, Description = "Erro interno desconhecido")]
        [HttpPut("{grupoId}")]
        public async Task Atualizar([FromRoute] int grupoId, [FromBody] GrupoRequest model)
            => await this.negocioGrupo.Atualizar(grupoId, model);

        /// <summary>
        /// Excluir
        /// </summary>
        [SwaggerResponse(200, "Sucesso")]
        [SwaggerResponse(503, Description = "API ou algum recurso que ela depende está fora do ar")]
        [SwaggerResponse(500, Description = "Erro interno desconhecido")]
        [HttpDelete("{grupoId}")]
        public async Task Excluir([FromRoute] int grupoId)
            => await this.negocioGrupo.Excluir(grupoId);
    }
}