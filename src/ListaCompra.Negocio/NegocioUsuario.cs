using ListaCompra.Modelo;
using ListaCompra.Modelo.Enumeradores;
using ListaCompra.Modelo.Tratamento;
using System.Collections.Generic;

namespace ListaCompra.Negocio
{
	/// <summary>
	/// Métodos de negócio de usuários
	/// </summary>
	public class NegocioUsuario : NegocioBase
	{
		/// <summary>
		/// Contrutor da classe de negócio de usuário
		/// </summary>
		public NegocioUsuario() : base("Usuario")
		{
		}

		/// <summary>
		/// Método de validação e criação de usuário
		/// </summary>
		/// <param name="usuario">Usuário a ser criado</param>
		/// <returns>Grupo padrão do usuário</returns>
		public Grupo Criar(Usuario usuario)
		{
			var retorno = 1;

            if (retorno != default(int))
            {
                var grupoPadrao = new Grupo()
                {
                    IdGrupo = 1,
                    Nome = string.Format("Meu Grupo - {0}", usuario.NickName),
                    Descricao = "Meu grupo padrão, crie listas e organize suas compras.",
                    Usuarios = new List<Usuario>()
                    {
                        new Usuario()
                        {
                            IdUsuario = retorno,
                            Nome = usuario.Nome,
                            NickName = usuario.Nome,
                            Imagem = usuario.Imagem
                        }
                    },
                    UsuariosPerfis = new Dictionary<int, Perfil>()
                };

                grupoPadrao.UsuariosPerfis.Add(retorno, Perfil.Proprietario);

                return grupoPadrao;
            }

			throw new NegocioExcecao(500, "Falha na criação do usuário");
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="usuario"></param>
		/// <param name="senha"></param>
		/// <returns></returns>
		public List<Grupo> Login(string usuario, string senha)
		{
			var retorno = new List<Grupo>();
			var autenticado = true;
			
			if (autenticado)
			{
                for (int i = 1; i < 4; i++)
                {
                    var grupoPadrao = new Grupo()
                    {
                        IdGrupo = 1,
                        Nome = string.Format("Meu Grupo - {0}", usuario),
                        Descricao = "Meu grupo padrão, crie listas e organize suas compras.",
                        Usuarios = new List<Usuario>()
                    {
                        new Usuario()
                        {
                            IdUsuario = i,
                            Nome = usuario,
                            NickName = usuario.ToUpper(),
                            Imagem = string.Empty
                        }
                    },
                        UsuariosPerfis = new Dictionary<int, Perfil>()
                    };

                    grupoPadrao.UsuariosPerfis.Add(i, (Perfil)i);

                    retorno.Add(grupoPadrao); 
                }
			}
			else
			{
				throw new NegocioExcecao(400, "Usuário ou senha incorretos");
			}

			return retorno;
		}
	}
}