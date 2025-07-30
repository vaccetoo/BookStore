using BookStore.Core.Contracts;
using BookStore.Core.Services;
using BookStore.Infrastructure.Common.Contracts;
using BookStore.Infrastructure.Data;
using BookStore.Web.Contracts;
using BookStore.Web.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
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
				throw new InvalidOperationException(string.Format(NotFound, "DefaultConnection"));

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
			services.AddScoped<IBookService, BookService>();
			services.AddScoped<IImageService, ImageService>();
			services.AddScoped<IEmailSender, EmailSender>();

			return services;
		}

		public static IServiceCollection AddApplicationIdentity(this IServiceCollection services)
		{
			services.AddIdentity<IdentityUser, IdentityRole>(options =>
			{
				options.SignIn.RequireConfirmedAccount = false;

			})
			.AddEntityFrameworkStores<ApplicationDbContext>();

			return services;
		}
	}
}
