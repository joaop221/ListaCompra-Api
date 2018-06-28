using System;
using System.Collections.Generic;
using System.Text;
using ListaCompra.Modelo;
using ListaCompra.Modelo.Enumeradores;
using ListaCompra.Modelo.Tratamento;

namespace ListaCompra.Negocio
{
	/// <summary>
	/// Métodos de negócio de lista de produtos
	/// </summary>
	public class NegocioLista : NegocioBase
	{
		/// <summary>
		/// Contrutor da classe de negócio de lista de produtos
		/// </summary>
		public NegocioLista() : base("Lista")
		{
		}

        /// <summary>
        /// Consulta lista no Id específicado
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Lista Consultar(int id)
        {
            var retorno = id;

            if (id != default(int))
            {
                var produtos = new List<Produto>();
                var random = new Random();

                for (int i = 1; i < 11; i++)
                {
                    produtos.Add(new Produto()
                    {
                        IdProduto = i,
                        Nome = string.Format("Teste - {0}", i),
                        Quantidade = random.Next(0, 11),
                        ValorUnitario = Math.Round(random.NextDouble() * 10, 2, MidpointRounding.AwayFromZero),
                        Status = Status.Confirmado,
                        UnidadeMedida = UnidadeMedida.UN
                    });
                }

                return new Lista()
                {
                    IdLista = id,
                    Descricao = "Lista de Compra Mensal",
                    Nome = "Mensal",
                    ListaProduto = produtos
                };
            }

            throw new NegocioExcecao(400, "Não foi possível encontrar a lista indicada");
        }
    }
}
