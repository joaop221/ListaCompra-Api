using System;
using System.Collections.Generic;
using System.Linq;

namespace ListaCompra.Modelo.Autorizacao
{
    /// <summary>
    /// Token de Autorização
    /// </summary>
    public class AuthToken : Dictionary<int, string>
    {
        /// <summary>
        /// Contrutor da classe de Autorização
        /// </summary>
        public AuthToken() : base()
        {
        }

        /// <summary>
        /// Define e consulta valor do token salvo em memória
        /// </summary>
        /// <param name="valor">Valor do token a ser inserido ou consultado</param>
        /// <returns>Passa ou consulta Id do usuário associado ao token</returns>
        public int this[String valor]
        {
            get
            {
                var valuePairs = this.Where(x => x.Value == valor);

				if (valuePairs.Count() == 0)
					return 0;
				else
					return valuePairs.FirstOrDefault().Key;
            }
			set
			{
				var valuePairs = this.Where(x => x.Key == value && x.Value == valor);

				if (valuePairs.Count() == 0)
					this.Add(value, valor);
				else
					this[value] = valor;
			}
        }
    }
}
