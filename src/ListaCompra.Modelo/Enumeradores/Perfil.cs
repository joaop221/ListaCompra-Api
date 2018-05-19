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
		Desconhecido = 0,

		/// <summary>
		/// Perfil de proprietário e com controles totais sobre o grupo
		/// </summary>
		Proprietario = 1,

		/// <summary>
		/// Perfil de administrador com controles parciais sobre o grupo
		/// </summary>
		Administrador = 2,

		/// <summary>
		/// Perfil comum
		/// </summary>
		Comum = 3
    }
}
