using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Text;

namespace ListaCompra.Modelo.Response.Elementos
{
	/// <summary>
	/// Modelo de auxílio do preenchimento de elementos na tela
	/// </summary>
	public class Helper
	{
		/// <summary>
		/// Construtor da classe de Auxílio de elementos
		/// </summary>
		public Helper(Type tipoEnumerador, bool addDescricao)
		{
			NomeElemento = tipoEnumerador.ToString();

			foreach (var enumItem in Enum.GetValues(tipoEnumerador))
			{
				if (addDescricao)
				{
					string descricao = null;

					FieldInfo fi = enumItem.GetType().GetField(enumItem.ToString());

					DescriptionAttribute[] attributes =
						(DescriptionAttribute[])fi.GetCustomAttributes(
						typeof(DescriptionAttribute),
						false);

					if (attributes != null &&
						attributes.Length > 0)
						descricao = attributes[0].Description;
					else
						descricao = enumItem.ToString();

					Descricao.Add((int)enumItem, descricao); 
				}

				Elementos.Add((int)enumItem, enumItem.ToString());
			}
		}

		/// <summary>
		/// Nome do elemento
		/// </summary>
		public string NomeElemento { get; set; }

		/// <summary>
		/// Coleção de elementos serializados do enumerador
		/// </summary>
		public Dictionary<int, string> Elementos { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Dictionary<int, string> Descricao { get; set; }
	}
}
