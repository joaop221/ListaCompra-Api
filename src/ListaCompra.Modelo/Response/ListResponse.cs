using System.Collections.Generic;

namespace ListaCompra.Modelo.Response
{
	/// <summary>
	/// Response padrão de listas
	/// </summary>
	public class ListResponse<TItem> : BaseResponse where TItem : new()
	{
		/// <summary>
		/// Item de retorno do objeto
		/// </summary>
		public List<TItem> Item { get; set; }
	}
}