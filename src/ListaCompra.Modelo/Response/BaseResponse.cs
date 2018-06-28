using ListaCompra.Modelo.Response.Elementos;
using System;
using System.Collections.Generic;

namespace ListaCompra.Modelo.Response
{
	/// <summary>
	/// Modelo padrão de response da aplicação
	/// </summary>
	public abstract class BaseResponse
	{
		/// <summary>
		/// Mensagem de retorno da consulta
		/// </summary>
		public virtual string Messagem { get; set; }

		/// <summary>
		/// Código de retorno para rastrabilidade da execução
		/// </summary>
		public virtual int CodigoRetorno { get; set; }

		/// <summary>
		/// Elementos a serem enviados para o front
		/// </summary>
		public virtual List<Helper> Elementos { get; set; }

        /// <summary>
        /// Caso ocorra excecao retorna serializada
        /// </summary>
        public virtual Exception Excecao { get; set; }
	}
}