using BookStore.Core.Contracts;
using BookStore.Core.Models.Book;
using BookStore.Infrastructure.Common.Messages;
using BookStore.Web.Extensions;
using BookStore.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using static BookStore.Infrastructure.Common.Messages.LoggMessages;
using static BookStore.Infrastructure.Common.Messages.TempDataMessages;

namespace BookStore.Web.Areas.Customer.Controllers
{
	[Area("Customer")]
	public class HomeController : Controller
	{
		private readonly IBookService _bookService;
		private readonly IShoppingCartService _shoppingCartService;
		private readonly ILogger<HomeController> _logger;

		public HomeController(ILogger<HomeController> logger,
			IShoppingCartService shoppingCartService,
			IBookService bookService)
		{
			_bookService = bookService;
			_shoppingCartService = shoppingCartService;
			_logger = logger;
		}

		[AllowAnonymous]
		public async Task<IActionResult> Index()
		{
			var model = await _bookService.GetAllViewModelAsync();

			return View(model);
		}

		[AllowAnonymous]
		public async Task<IActionResult> Details(int? id)
		{
			try
			{
				var model = await _bookService.GetDetailsViewModelByIdAsync(id);

				return View(model);
			}
			catch (Exception ex)
			{
				return NotFound(ex.Message);
			}
		}

		[HttpPost]
		[Authorize]
		public async Task<IActionResult> Details(int bookId, int count)
		{
			if (count < 1)
			{
				_logger.LogWarning(LoggMessages.InvalidCountAttempt);

				TempData["error"] = TempDataMessages.InvalidQuantity;

				return RedirectToAction(nameof(Details), new { id = bookId });
			}

			var userId = User.GetUserId();

			if(userId == null)
			{
				_logger.LogWarning(LoggMessages.NullIdSelected,
					nameof(HomeController),
					nameof(Details));

				return Unauthorized();
			}

			try
			{
				await _shoppingCartService.AddToCartAsync(bookId, count, userId);

				TempData["success"] = SuccessfullyAddedToCart;

				return RedirectToAction(nameof(Index));
			}
			catch (Exception ex)
			{
				TempData["error"] = ex.Message;

				return RedirectToAction(nameof(Details), new { id = bookId });
			}
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
