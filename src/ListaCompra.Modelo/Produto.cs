using FluentValidation.Attributes;
using ListaCompra.Modelo.Enumeradores;
using ListaCompra.Modelo.Validator;
using System;

namespace ListaCompra.Modelo
{
	/// <summary>
	/// Modelo de armazenamento de produto
	/// </summary>
	[Validator(typeof(ProdutoValidator))]
	public class Produto : ProdutoBase
    {
		/// <summary>
		/// Unidade de medida do produto
		/// </summary>
		public UnidadeMedida UnidadeMedida { get; set; }

		/// <summary>
		/// Status do produto
		/// </summary>
		public Status Status { get; set; }

		/// <summary>
		/// Quantidade de produtos
		/// </summary>
		public int Quantidade { get; set; }

		/// <summary>
		/// Valor unitário estimado
		/// </summary>
		public double ValorUnitario { get; set; }

		/// <summary>
		/// Valor total dos produtos
		/// </summary>
		public double ValorTotal
		{
			get => Math.Round(ValorUnitario * Quantidade, 2, MidpointRounding.AwayFromZero);
		}
	}
}