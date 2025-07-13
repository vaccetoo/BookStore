using BookStore.Core.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Web.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class GenreController : Controller
	{
		private readonly IGenreService _genreService;

		public GenreController(IGenreService genreService)
		{
			_genreService = genreService;
		}

		public async Task<IActionResult> Index()
		{
			return View();
		}
	}
}
