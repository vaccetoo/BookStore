using BookStore.Infrastructure.Data.Configuration;
using BookStore.Infrastructure.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Infrastructure.Data
{
	public class ApplicationDbContext : IdentityDbContext<IdentityUser>
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
			: base(options)
		{
		}

		public DbSet<Genre> Genres { get; set; } = null!;
		public DbSet<Book> Books { get; set; } = null!;

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfiguration(new GenreConfiguration());
			modelBuilder.ApplyConfiguration(new BookConfiguration());

			base.OnModelCreating(modelBuilder);
		}
	}
}
