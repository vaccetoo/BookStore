using BookStore.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookStore.Infrastructure.Data.Configuration
{
	internal class GenreConfiguration : IEntityTypeConfiguration<Genre>
	{
		public void Configure(EntityTypeBuilder<Genre> builder)
		{
			builder.HasData(
				new Genre { Id = 1, Name = "Fantasy", DisplayOrder = 1 },
				new Genre { Id = 2, Name = "Biography ", DisplayOrder = 2 },
				new Genre { Id = 3, Name = "History", DisplayOrder = 3 },
				new Genre { Id = 4, Name = "Science", DisplayOrder = 4 },
				new Genre { Id = 5, Name = "Science Fiction", DisplayOrder = 5 },
				new Genre { Id = 6, Name = "Philosophy", DisplayOrder = 6 });
		}
	}
}
