using BookStore.Core.Contracts;
using BookStore.Core.Models.Genre;
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

		[HttpGet]
		public async Task<IActionResult> Index()
		{
			IEnumerable<GenreViewModel> model = await _genreService.AllGenresAsync();

			return View(model);
		}
	}
}
