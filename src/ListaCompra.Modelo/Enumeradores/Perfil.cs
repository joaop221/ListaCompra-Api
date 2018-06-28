using System.ComponentModel;

namespace ListaCompra.Modelo.Enumeradores
{
	/// <summary>
	/// Modelo de gerenciamento de perfis de grupos
	/// </summary>
	public enum Perfil
	{
		/// <summary>
		/// Perfil desconhecido ou não definido
		/// </summary>
		[Description("Perfil desconhecido ou não definido")]
		Desconhecido = 0,

		/// <summary>
		/// Perfil de proprietário e com controles totais sobre o grupo
		/// </summary>
		[Description("Perfil de proprietário e com controles totais sobre o grupo")]
		Proprietario = 1,

		/// <summary>
		/// Perfil de administrador com controles parciais sobre o grupo
		/// </summary>
		[Description("Perfil de administrador com controles parciais sobre o grupo")]
		Administrador = 2,

		/// <summary>
		/// Perfil comum
		/// </summary>
		[Description("Perfil comum")]
		Comum = 3
	}
}