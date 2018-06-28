using FluentValidation.Attributes;
using ListaCompra.Modelo.Enumeradores;
using ListaCompra.Modelo.Validator;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ListaCompra.Modelo
{
	/// <summary>
	/// Lista de Produtos
	/// </summary>
	[Validator(typeof(ListaValidator))]
	public class Lista : Base
	{
		/// <summary>
		/// Id da Lista
		/// </summary>
		public int IdLista { get; set; }

		/// <summary>
		/// Nome da Lista
		/// </summary>
		public string Nome { get; set; }

		/// <summary>
		/// Descrição da Lista
		/// </summary>
		public string Descricao { get; set; }

        /// <summary>
        /// Icone de identificação do grupo
        /// </summary>
        public string Imagem { get; set; }

        /// <summary>
        /// Lista de Produtos
        /// </summary>
        public List<Produto> ListaProduto { get; set; }

		/// <summary>
		/// Valor total dos produtos
		/// </summary>
		public double ValorTotal
		{
			get => Math.Round(ListaProduto.Sum(x => x.ValorTotal), 2, MidpointRounding.AwayFromZero);
		}

		/// <summary>
		/// Se lista esta concluída
		/// </summary>
		public bool Concluida
		{
			get => ListaProduto.Any(x => x.Status != Status.Confirmado);
		}
	}
}