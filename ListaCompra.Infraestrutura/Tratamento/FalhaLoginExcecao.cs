using System;

namespace ListaCompra.Infraestrutura.Tratamento
{
    public class FalhaLoginExcecao : Exception
    {
        public FalhaLoginExcecao(string msg) : base(msg)
        {
        }
    }
}