using System.ComponentModel;

namespace ListaCompra.Modelo.Enumeradores
{
	/// <summary>
	/// Modelo representante do provedor de identidade
	/// </summary>
	public enum Provedor
	{
		/// <summary>
		/// Provedor de identidade desconhecido ou não definido
		/// </summary>
		[Description("Provedor de identidade desconhecido ou não definido")]
		Desconhecido = 0,

		/// <summary>
		/// Google provider - OAuth 2.0
		/// </summary>
		[Description("Google provider - OAuth 2.0")]
		Google = 1,

		/// <summary>
		/// Facebook provider -
		/// </summary>
		[Description("Facebook provider - ")]
		Facebook = 2,
	}
}