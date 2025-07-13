using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static BookStore.Infrastructure.Common.Constants.ValidationConstants;

namespace BookStore.Infrastructure.Data.Models
{
	[Comment("Book")]
	public class Book
	{
		[Comment("Book unique identifier")]
		[Required]
		public int Id { get; set; }

		[Comment("Name of the Author")]
		[Required]
		[MaxLength(AuthorMaxLength)]
		public string Author { get; set; } = null!;

		[Comment("Book Title")]
		[Required]
		[MaxLength(BookTitleMaxLength)]
		public string Title { get; set; } = null!;

		[Comment("Book Description")]
		[Required]
		[MaxLength (BookDescriptionMaxLength)]
		public string Description { get; set; } = null!;

		[Comment("International Standard Book Number")]
		[Required]
		[MaxLength(BookIsbnMaxLength)]
		public string ISBN { get; set; } = null!;

		[Comment("Price of the Book")]
		[Required]
		[Precision(18, 2)]
		public decimal Price { get; set; }

		[Comment("Photo of the book")]
		public string? ImageUrl { get; set; }


		[Comment("Genre unique identifier")]
		[Required]
		public int GenreId { get; set; }
		[ForeignKey(nameof(GenreId	))]
		public Genre Genre { get; set; } = null!;
	}
}
