using BookStore.Core.Contracts;
using BookStore.Core.Models.Book;
using BookStore.Infrastructure.Common.Messages;
using BookStore.Web.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static BookStore.Infrastructure.Common.Constants.RoleConstants;

namespace BookStore.Web.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Authorize(Roles = RoleAdmin)]
	public class BookController : Controller
	{
		private readonly IBookService _bookService;
		private readonly IGenreService _genreService;
		private readonly IImageService _imageService;
		private readonly ILogger<BookController> _logger;

		public BookController(
			IBookService bookService,
			IGenreService genreService,
			IImageService imageService,
			ILogger<BookController> logger)
		{
			_bookService = bookService;
			_genreService = genreService;
			_imageService = imageService;
			_logger = logger;
		}

		[HttpGet]
		public async Task<IActionResult> Index()
		{
			var model = await _bookService.GetAllViewModelAsync();

			return View(model);
		}

		[HttpGet]
		public async Task<IActionResult> Create()
		{
			var model = new BookCreateFormModel
			{
				Genres = await _genreService.GetAllViewModelAsync()
			};

			return View(model);
		}

		[HttpPost]
		public async Task<IActionResult> Create(BookCreateFormModel model, IFormFile? file)
		{
			if (!ModelState.IsValid)
			{
				_logger.LogWarning(LoggMessages.InvalidModelState, 
					nameof(BookCreateFormModel), nameof(BookController), 
					nameof(Create));

				model.Genres = await _genreService.GetAllViewModelAsync();

				return View(model);
			}

			try
			{
				if (file != null && file.Length > 0)
				{
					model.ImageUrl = _imageService.SaveImage(file, "images/books");
				}

				await _bookService.AddBookAsync(model);

				TempData["success"] = string.Format(TempDataMessages.SuccessCreated, "Book");

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
				var model = await _bookService.GetEditModelByIdAsync(id);

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
		public async Task<IActionResult> Edit(BookEditFormModel model, IFormFile? file)
		{
			if (!ModelState.IsValid)
			{
				_logger.LogWarning(LoggMessages.InvalidModelState, 
					nameof(BookEditFormModel), nameof(BookController), 
					nameof(Edit));

				model.Genres = await _genreService.GetAllViewModelAsync();

				return View(model);
			}

			try
			{
				if (file != null && file.Length > 0)
				{
					if (!string.IsNullOrWhiteSpace(model.ImageUrl))
					{
						_imageService.DeleteImage(model.ImageUrl);
					}

					model.ImageUrl = _imageService.SaveImage(file, "images/books");
				}

				await _bookService.EditBookAsync(model);

				TempData["success"] = string.Format(TempDataMessages.SuccessUpdated, "Book");

				return RedirectToAction(nameof(Index));
			}
			catch (Exception ex)
			{
				TempData["error"] = ex.Message;

				model.Genres = await _genreService.GetAllViewModelAsync();

				return View(model);
			}
		}




		#region API CALLS
		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			var model = await _bookService.GetAllViewModelAsync();

			return Json(new {data = model});
		}

		[HttpDelete]		
		
		public async Task<IActionResult> Delete(int? id)
		{
			try
			{
				var model = await _bookService.GetViewModelByIdAsync(id);

				if (!string.IsNullOrWhiteSpace(model.ImageUrl))
				{
					_imageService.DeleteImage(model.ImageUrl);
				}

				await _bookService.DeleteBookAsync(id);

				return Json(new { success = true, message = string.Format(TempDataMessages.SuccessfullyDeleted, "Book") });
			}
			catch (Exception ex)
			{
				return Json(new { success = false, message = ex.Message });
			}
		}

		#endregion
	}
}
