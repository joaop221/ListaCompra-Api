using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ListaCompra.Modelo;
using ListaCompra.Negocio;
using Microsoft.AspNetCore.Mvc;

namespace ListaCompra.Servicos.Controllers
{
	[Route("api/[controller]")]
	public class UsuarioController : Controller
	{
		private NegocioUsuario negocioUsuario;

		public UsuarioController()
		{
			this.negocioUsuario = new NegocioUsuario();
		}

		[HttpGet]
		public string Index()
		{
			return "Hello";
		}

		public bool Criar(Usuario usuario)
		{
			return this.negocioUsuario.Criar(usuario);
		}
	}
}