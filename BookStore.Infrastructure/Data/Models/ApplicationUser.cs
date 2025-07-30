using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using static BookStore.Infrastructure.Common.Constants.ValidationConstants;

namespace BookStore.Infrastructure.Data.Models
{
	[Comment("Extended IdentityUser")]
	public class ApplicationUser : IdentityUser
	{
		[Comment("Real Name of the User")]
		[Required]
		[MaxLength(NameMaxLength)]
		public string Name { get; set; } = null!;

		[Comment("Address of the User")]
		[MaxLength(AddressMaxLength)]
		public string? Address { get; set; }

		[Comment("City of the user")]
		[MaxLength(CityMaxLength)]
		public string? City { get; set; }

		[Comment("Post code of the user")]
		[MaxLength(PostCodeMaxLength)]
		public string? PostCode { get; set; }
	}
}
