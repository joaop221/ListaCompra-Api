using ListaCompra.Dados;
using System;

namespace ListaCompra.Negocio
{
    public class NegocioBase
    {
		private DadosBase dadosBase;

		public NegocioBase()
		{
			dadosBase = new DadosBase();
		}
    }
}
