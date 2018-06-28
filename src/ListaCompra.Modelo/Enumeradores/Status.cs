using System.ComponentModel;

namespace ListaCompra.Modelo.Enumeradores
{
	/// <summary>
	/// Modelo de status de produto
	/// </summary>
	public enum Status
	{
		/// <summary>
		/// Produto pendente de aprovação
		/// </summary>
		[Description("Pendente aprovação")]
		Pendente = 0,

		/// <summary>
		/// Produto confirmado na lista
		/// </summary>
		[Description("Confirmado na lista")]
		Confirmado = 1,

		/// <summary>
		/// Produto comprado
		/// </summary>
		[Description("Comprado")]
		Comprado = 2
	}
}