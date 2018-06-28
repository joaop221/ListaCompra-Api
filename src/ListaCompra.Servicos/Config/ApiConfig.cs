using ListaCompra.Dados.Agente;
using ListaCompra.Modelo.Notificacoes;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ListaCompra.Servicos.Config
{
	/// <summary>
	/// Inicializa e configura configurações do cache de configurações
	/// </summary>
    public static class ApiConfig
    {
		/// <summary>
		/// Adiciona serviço de cache na aplicação - destinado a cache de notificações
		/// </summary>
		/// <param name="services">servicos da aplicação</param>
		public static void AddSingletons(this IServiceCollection services)
		{
			services.AddMemoryCache(option =>
			{
				option.SizeLimit = int.MaxValue;
			});

			services.AddSingleton<NotificacaoCache>();
			services.AddSingleton<AgenteBase>();
			services.AddSingleton<DbContext>();
		}
    }
}
