using BookStore.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookStore.Infrastructure.Data.Configuration
{
	internal class BookConfiguration : IEntityTypeConfiguration<Book>
	{
		public void Configure(EntityTypeBuilder<Book> builder)
		{
			builder.HasData(
				new Book
				{
					Id = 1,
					Author = "J.R.R. Tolkien",
					Title = "The Hobbit",
					Description = "A fantasy adventure about a hobbit's journey to reclaim treasure guarded by a dragon.",
					ISBN = "9780261102217",
					Price = 20.00m,
					ImageUrl = null,
					GenreId = 1
				},
				new Book
				{
					Id = 2,
					Author = "Michelle Obama",
					Title = "Becoming",
					Description = "A memoir by the former First Lady of the United States.",
					ISBN = "9781524763138",
					Price = 25.00m,
					ImageUrl = null,
					GenreId = 2 
				},
				new Book
				{
					Id = 3,
					Author = "Yuval Noah Harari",
					Title = "Sapiens",
					Description = "A brief history of humankind from ancient times to the modern age.",
					ISBN = "9780062316097",
					Price = 20.00m,
					ImageUrl = null,
					GenreId = 3
				},
				new Book
				{
					Id = 4,
					Author = "Carl Sagan",
					Title = "Cosmos",
					Description = "A science book exploring the universe and our place in it.",
					ISBN = "9780345539434",
					Price = 19.00m,
					ImageUrl = null,
					GenreId = 4 
				},
				new Book
				{
					Id = 5,
					Author = "Isaac Asimov",
					Title = "Foundation",
					Description = "A sci-fi classic about the fall and rebuilding of a galactic empire.",
					ISBN = "9780553293357",
					Price = 15.00m,
					ImageUrl = null,
					GenreId = 5 
				},
				new Book
				{
					Id = 6,
					Author = "Plato",
					Title = "The Republic",
					Description = "A philosophical dialogue about justice and the ideal state.",
					ISBN = "9780140455113",
					Price = 12.50m,
					ImageUrl = null,
					GenreId = 6 
				}
			);
		}
	}
}
