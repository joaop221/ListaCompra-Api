using ListaCompra.Dados;
using System;

namespace ListaCompra.Negocio
{
	/// <summary>
	/// Classe comum a todas as entidades de negócio
	/// </summary>
    public class NegocioBase
    {
		/// <summary>
		/// Armazena entidades de dados
		/// </summary>
		private DadosBase dadosBase;

		/// <summary>
		/// Construtor instancia dados base
		/// </summary>
		public NegocioBase()
		{
			dadosBase = new DadosBase();
		}
    }
}
