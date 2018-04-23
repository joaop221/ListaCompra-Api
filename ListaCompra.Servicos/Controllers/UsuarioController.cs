using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ListaCompra.Modelo;
using Microsoft.AspNetCore.Mvc;

namespace ListaCompra.Servicos.Controllers
{
	[Route("api/[controller]")]
	public class UsuarioController : Controller
	{
		[HttpGet]
		public string Index()
		{
			return "Hello";
		}

		public bool Criar(Usuario usuario)
		{
			
		}
	}
}