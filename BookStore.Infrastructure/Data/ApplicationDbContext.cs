using BookStore.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Infrastructure.Data
{
	public class ApplicationDbContext : DbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
			: base(options)
		{
		}

		public DbSet<Genre> Genres { get; set; } = null!;
	}
}
