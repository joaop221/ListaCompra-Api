using ListaCompra.Modelo.Autorizacao;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace ListaCompra.Servicos
{
	/// <summary>
	/// Classe de inicialização da aplicação
	/// </summary>
	public class Program
	{
        /// <summary>
        /// Método de inicialização da aplicação
        /// </summary>
        /// <param name="args">Argumentos de entrada</param>
        public static void Main(string[] args)
		{
			BuildWebHost(args).Run();
		}

		/// <summary>
		/// Método de construção da aplicação, inicializa serviços e configurações
		/// </summary>
		/// <param name="args">Argumento de estrada</param>
		/// <returns>Web Api inicializada</returns>
		public static IWebHost BuildWebHost(string[] args) =>
			WebHost.CreateDefaultBuilder(args)
				.UseStartup<Startup>()
				.Build();
	}
}