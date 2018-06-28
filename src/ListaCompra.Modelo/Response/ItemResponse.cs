namespace ListaCompra.Modelo.Response
{
	/// <summary>
	/// Response padrão de itens
	/// </summary>
	public class ItemResponse<TItem> : BaseResponse where TItem : new()
	{
		/// <summary>
		/// Item de retorno do objeto
		/// </summary>
		public TItem Item { get; set; }
	}
}