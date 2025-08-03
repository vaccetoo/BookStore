using System.Security.Claims;

namespace BookStore.Web.Extensions
{
	public static class ClaimsPrincipalExtension
	{
		public static string? GetUserId(this ClaimsPrincipal user)
		{
			return user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
		}
	}
}
