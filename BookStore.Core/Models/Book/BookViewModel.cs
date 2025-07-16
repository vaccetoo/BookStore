using BookStore.Core.Models.Genre;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStore.Core.Models.Book
{
	public class BookViewModel
	{
		public int Id { get; set; }

		public string Author { get; set; } = null!;

		public string Title { get; set; } = null!;

		public string Description { get; set; } = null!;

		public string ISBN { get; set; } = null!;

		public decimal Price { get; set; }

		public string? ImageUrl { get; set; }

		public string Genre { get; set; } = null!;

		public int GenreId { get; set; }	
	}
}
