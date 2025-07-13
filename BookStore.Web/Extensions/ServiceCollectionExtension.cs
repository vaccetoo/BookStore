using BookStore.Core.Contracts;
using BookStore.Core.Services;
using BookStore.Infrastructure.Common.Contracts;
using BookStore.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using static BookStore.Infrastructure.Common.Messages.ExceptionMessages;

namespace BookStore.Web.Extensions
{
	public static class ServiceCollectionExtension
	{
		public static IServiceCollection AddApplicationDbContext(this IServiceCollection services,
			IConfiguration configuration)
		{
			string connectionString = configuration.GetConnectionString("DefaultConnection") ??
				throw new InvalidOperationException(ConnectionStringNotFound);

			services.AddDbContext<ApplicationDbContext>(options =>
			{
				options.UseSqlServer(connectionString);
			});

			return services;
		}

		public static IServiceCollection AddApplicationServices(this IServiceCollection services)
		{
			services.AddScoped<IUnitOfWork, UnitOfWork>();
			services.AddScoped<IGenreService, GenreService>();

			return services;
		}
	}
}
