using BookStore.Core.Models.Genre;
using System.ComponentModel.DataAnnotations;
using static BookStore.Infrastructure.Common.Messages.ConstraintUserMessages;
using static BookStore.Infrastructure.Common.Constants.ValidationConstants;

namespace BookStore.Core.Models.Book
{
	public class BookCreateFormModel
	{
		[Required(ErrorMessage = RequiredMessage)]
		[StringLength(BookTitleMaxLength,
			MinimumLength = BookTitleMinLength,
			ErrorMessage = StringLengthMessage)]
		public string Title { get; set; } = null!;

		[Required(ErrorMessage = RequiredMessage)]
		[StringLength(AuthorMaxLength,
			MinimumLength = AuthorMinLength,
			ErrorMessage = StringLengthMessage)]
		public string Author { get; set; } = null!;

		[Required(ErrorMessage = RequiredMessage)]
		[StringLength(BookDescriptionMaxLength,
			MinimumLength = BookDescriptionMinLength,
			ErrorMessage = StringLengthMessage)]
		public string Description { get; set; } = null!;

		[Required(ErrorMessage = RequiredMessage)]
		[StringLength(BookIsbnMaxLength,
			MinimumLength = BookIsbnMinLength,
			ErrorMessage = StringLengthMessage)]
		public string ISBN { get; set; } = null!;

		[Required(ErrorMessage = RequiredMessage)]
		[Range(BookMinPrice,BookMaxPrice,
			ErrorMessage = RangeMessage)]
		public decimal Price { get; set; }

		[Display(Name = "Image")]
		public string? ImageUrl { get; set; }

		[Required(ErrorMessage = RequiredMessage)]
		[Display(Name = "Genre")]
		public int GenreId { get; set; }
		public IEnumerable<GenreViewModel> Genres { get; set; } 
			= new List<GenreViewModel>();
	}
}
