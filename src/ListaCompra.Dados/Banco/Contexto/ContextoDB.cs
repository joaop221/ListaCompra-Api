using ListaCompra.Modelo;
using Microsoft.EntityFrameworkCore;
using System;

namespace ListaCompra.Dados.Banco.Contexto
{
	/// <summary>
	/// Contexto de Banco de dados da Aplicação
	/// </summary>
	public partial class ContextoDB : DbContext
	{
		/// <summary>
		/// Indica se uma opração está em execução
		/// </summary>
		public Boolean ExecutandoTransacao { get; set; }

		/// <summary>
		/// Construtor do contexto do banco
		/// </summary>
		/// <param name="options">Opções de inicialização</param>
		public ContextoDB(DbContextOptions<ContextoDB> options) : base(options)
		{
		}

		#region Tabelas Gerais

		/// <summary>
		/// Grupos
		/// </summary>
		public virtual DbSet<Grupo> Grupos { get; set; }

		/// <summary>
		/// Listas
		/// </summary>
		public virtual DbSet<Lista> Listas { get; set; }

		/// <summary>
		/// Produtos
		/// </summary>
		public virtual DbSet<Produto> Produtos { get; set; }

		/// <summary>
		/// Usuarios Provedor
		/// </summary>
        public virtual DbSet<UsuarioHelper> UsuariosProvedor { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public virtual DbSet<Usuario> Usuarios { get; set; }

		#endregion Tabelas Gerais
	}
}