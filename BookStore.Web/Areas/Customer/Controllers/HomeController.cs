using BookStore.Core.Contracts;
using BookStore.Core.Models.Book;
using BookStore.Core.Models.Genre;
using BookStore.Core.Services;
using BookStore.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BookStore.Web.Areas.Customer.Controllers
{
	[Area("Customer")]
	public class HomeController : Controller
	{
		private readonly IBookService _bookService;
		private readonly ILogger<HomeController> _logger;

		public HomeController(ILogger<HomeController> logger,
			IBookService bookService)
		{
			_logger = logger;
			_bookService = bookService;
		}

		public async Task<IActionResult> Index()
		{
			var model = await _bookService.GetAllViewModelAsync();

			return View(model);
		}

		public async Task<IActionResult> Details(int? id)
		{
			try
			{
				var model = await _bookService.GetViewModelByIdAsync(id);

				return View(model);
			}
			catch (Exception ex)
			{
				return NotFound(ex.Message);
			}
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
