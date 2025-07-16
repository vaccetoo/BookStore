using BookStore.Core.Contracts;
using BookStore.Core.Models.Book;
using BookStore.Core.Models.Genre;
using BookStore.Infrastructure.Common.Messages;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Web.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class BookController : Controller
	{
		private readonly IBookService _bookService;
		private readonly IGenreService _genreService;
		private readonly ILogger<BookController> _logger;

		public BookController(IBookService bookService,
			IGenreService genreService,
			ILogger<BookController> logger)
		{
			_bookService = bookService;
			_genreService = genreService;
			_logger = logger;
		}

		[HttpGet]
		public async Task<IActionResult> Index()
		{
			IEnumerable<BookViewModel> model = await _bookService.GetAllViewModelAsync();

			return View(model);
		}

		[HttpGet]
		public async Task<IActionResult> Create()
		{
			BookCreateFormModel model = new();

			model.Genres = await _genreService.GetAllViewModelAsync();

			return View(model);
		}

		[HttpPost]
		public async Task<IActionResult> Create(BookCreateFormModel model)
		{
			if (!ModelState.IsValid)
			{
				_logger.LogWarning(LoggMessages.InvalidModelState,
					nameof(BookCreateFormModel),
					nameof(BookController),
					nameof(Create));

				model.Genres = await _genreService.GetAllViewModelAsync();

				return View(model);
			}

			try
			{
				await _bookService.AddBookAsync(model);

				TempData["success"] = string.Format(TempDataMessages.SuccessCreated,
					"Book");

				return RedirectToAction(nameof(Index));
			}
			catch (Exception ex)
			{
				TempData["error"] = ex.Message;

				model.Genres = await _genreService.GetAllViewModelAsync();

				return View(model);
			}
		}

		[HttpGet]
		public async Task<IActionResult> Edit(int? id)
		{
			try
			{
				BookEditFormModel model = await _bookService.GetEditModelByIdAsync(id);

				model.Genres = await _genreService.GetAllViewModelAsync();

				return View(model);
			}
			catch (Exception ex)
			{
				TempData["error"] = ex.Message;

				return RedirectToAction(nameof(Index));
			}
		}

		[HttpPost]
		public async Task<IActionResult> Edit(BookEditFormModel model)
		{
			if (!ModelState.IsValid)
			{
				_logger.LogWarning(LoggMessages.InvalidModelState,
					nameof(BookEditFormModel),
					nameof(BookController),
					nameof(Edit));

				model.Genres = await _genreService.GetAllViewModelAsync();

				return View(model);
			}

			try
			{
				await _bookService.EditBookAsync(model);

				TempData["success"] = string.Format(TempDataMessages.SuccessUpdated,
					"Book");

				return RedirectToAction(nameof(Index));
			}
			catch (Exception ex)
			{
				TempData["error"] = ex.Message;

				model.Genres = await _genreService.GetAllViewModelAsync();

				return View(model);
			}
		}

		[HttpGet]
		public async Task<IActionResult> Delete(int? id)
		{
			try
			{
				BookViewModel model = await _bookService.GetViewModelByIdAsync(id);
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
				await _bookService.DeleteBookAsync(id);

				TempData["success"] = string.Format(TempDataMessages.SuccessfullyDeleted,
					"Book");

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
