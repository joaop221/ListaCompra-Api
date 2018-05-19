using ListaCompra.Modelo;
using System;
using System.Collections.Generic;
using System.Text;

namespace ListaCompra.Negocio
{
	/// <summary>
	/// Métodos de negócio de usuários
	/// </summary>
    public class NegocioUsuario : NegocioBase
    {
		/// <summary>
		/// Método de validação e criação de usuário
		/// </summary>
		/// <param name="usuario"></param>
		/// <returns></returns>
		public bool Criar(Usuario usuario)
		{
			return true;
		}
    }
}
