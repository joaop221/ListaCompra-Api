using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ListaCompra.Servicos.Controllers
{
	[Route("api/[controller]")]
	public class ListaController : Controller
	{
		[HttpGet]
		public string Index()
		{
			return "Hello";
		}
	}
}
