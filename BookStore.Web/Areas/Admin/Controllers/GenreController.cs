using BookStore.Core.Contracts;
using BookStore.Core.Models.Genre;
using BookStore.Infrastructure.Common.Messages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static BookStore.Infrastructure.Common.Constants.RoleConstants;

namespace BookStore.Web.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Authorize(Roles = RoleAdmin)]
	public class GenreController : Controller
	{
		private readonly IGenreService _genreService;
		private readonly ILogger<GenreController> _logger;

		public GenreController(IGenreService genreService,
			ILogger<GenreController> logger)
		{
			_genreService = genreService;
			_logger = logger;
		}

		[HttpGet]
		public async Task<IActionResult> Index()
		{
			IEnumerable<GenreViewModel> model = await _genreService.GetAllViewModelAsync();

			return View(model);
		}

		[HttpGet]
		public IActionResult Create()
		{
			GenreCreateFormModel model = new();

			return View(model);
		}

		[HttpPost]
		public async Task<IActionResult> Create(GenreCreateFormModel model)
		{
			if (!ModelState.IsValid)
			{
				_logger.LogWarning(LoggMessages.InvalidModelState,
					nameof(GenreCreateFormModel),
					nameof(GenreController),
					nameof(Create));

				return View(model);
			}

			try
			{
				await _genreService.AddGenreAsync(model);

				TempData["success"] = string.Format(TempDataMessages.SuccessCreated,
					"Genre");

				return RedirectToAction(nameof(Index));
			}
			catch (Exception ex)
			{
				TempData["error"] = ex.Message;

				return View(model);
			}
		}

		[HttpGet]
		public async Task<IActionResult> Edit(int? id)
		{
			try
			{
				GenreEditFormModel model = await _genreService.GetEditModelByIdAsync(id);
				return View(model);
			}
			catch (Exception ex)
			{
				TempData["error"] = ex.Message;

				return RedirectToAction(nameof(Index));
			}
		}

		[HttpPost]
		public async Task<IActionResult> Edit(GenreEditFormModel model)
		{
			if (!ModelState.IsValid)
			{
				_logger.LogWarning(LoggMessages.InvalidModelState,
					nameof(GenreEditFormModel),
					nameof(GenreController),
					nameof(Edit));

				return View(model);
			}

			try
			{
				await _genreService.EditGenreAsync(model);

				TempData["success"] = string.Format(TempDataMessages.SuccessUpdated, 
					"Genre");

				return RedirectToAction(nameof(Index));
			}
			catch (Exception ex)
			{
				TempData["error"] = ex.Message;

				return View(model);
			}
		}

		[HttpGet]
		public async Task<IActionResult> Delete(int? id)
		{
			try
			{
				GenreViewModel model = await _genreService.GetViewModelByIdAsync(id);
				return View(model);
			}
			catch (Exception ex)
			{
				TempData["error"] = ex.Message;
			}

			return RedirectToAction(nameof(Index));
		}

		[HttpPost, ActionName("Delete")]
		public async Task<IActionResult> DeleteConfirmed(int? id)
		{
			try
			{
				await _genreService.DeleteGenreAsync(id);

				TempData["success"] = string.Format(TempDataMessages.SuccessfullyDeleted,
					"Genre");

				return RedirectToAction(nameof(Index));
			}
			catch (Exception ex)
			{
				TempData["error"] = ex.Message;

				return View();
			}
		}
	}
}
