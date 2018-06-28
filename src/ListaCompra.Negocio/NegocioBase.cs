using ListaCompra.Dados.Agente;
using ListaCompra.Dados.Banco;
using Microsoft.EntityFrameworkCore;

namespace ListaCompra.Negocio
{
	/// <summary>
	/// Classe comum a todas as entidades de negócio
	/// </summary>
	public abstract class NegocioBase
	{
		/// <summary>
		/// Armazena entidades de dados
		/// </summary>
		protected DbContext dadosBase;

		/// <summary>
		/// Armazena entidades de agentes
		/// </summary>
		protected AgenteBase agenteBase;

		/// <summary>
		/// Construtor de negócio base
		/// </summary>
		public NegocioBase(string metodo)
		{
			switch (metodo)
			{
				case "Usuario":
					this.dadosBase = new RepositorioUsuario();
					this.agenteBase = new AgenteBase();
					break;

				case "Grupo":
					this.dadosBase = new RepositorioGrupo();
					this.agenteBase = new AgenteBase();
					break;

				case "Lista":
					this.dadosBase = new RepositorioLista();
					this.agenteBase = new AgenteBase();
					break;

				case "Produto":
					this.dadosBase = new RepositorioProduto();
					this.agenteBase = new AgenteBase();
					break;

				case "Notificacao":
					this.dadosBase = new RepositorioNotificacao();
					this.agenteBase = new AgenteBase();
					break;

				default:
					this.agenteBase = new AgenteBase();
					break;
			}
		}
	}
}