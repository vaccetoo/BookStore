using BookStore.Core.Contracts;
using BookStore.Infrastructure.Common.Messages;
using BookStore.Web.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static BookStore.Infrastructure.Common.Messages.TempDataMessages;

namespace BookStore.Web.Areas.Customer.Controllers
{
	[Area("Customer")]
	[Authorize]
	public class ShoppingCartController : Controller
	{
		private readonly ILogger<ShoppingCartController> _logger;
		private readonly IShoppingCartService _shoppingCartService;

		public ShoppingCartController(IShoppingCartService shoppingCartService,
			ILogger<ShoppingCartController> logger)
		{
			_shoppingCartService = shoppingCartService;
			_logger = logger;
		}

		[HttpGet]
		public async Task<IActionResult> Index()
		{
			var userId = User.GetUserId();

			if (userId == null)
			{
				_logger.LogWarning(LoggMessages.NullIdSelected,
					nameof(ShoppingCartController),
					nameof(Index));

				return Unauthorized();
			}

			try
			{
				var model = await _shoppingCartService.GetShoppingCartByUserIdAsync(userId);

				return View(model);
			}
			catch (Exception ex)
			{
				_logger.LogError(LoggMessages.UnexpextedError,
					nameof(ShoppingCartController),
					nameof(Index));

				TempData["error"] = ex.Message;

				return RedirectToAction(nameof(Index), "Home");
			}
			
		}

		[HttpGet]
		public async Task<IActionResult> Plus(int? cartId)
		{
			if (cartId == null)
			{
				_logger.LogError(LoggMessages.NullIdSelected,
					nameof(ShoppingCartController),
					nameof(Plus));

				TempData["error"] = TempDataMessages.InvalidItem;
				return RedirectToAction(nameof(Index), "Home");
			}

			try
			{
				await _shoppingCartService.IncreaseCountAsync(cartId);

				return RedirectToAction(nameof(Index));
			}
			catch (Exception ex)
			{
				TempData["error"] = ex.Message;

				return RedirectToAction(nameof(Index), "Home");
			}

		}

		[HttpGet]
		public async Task<IActionResult> Minus(int? cartId)
		{
			if (cartId == null)
			{
				_logger.LogError(LoggMessages.NullIdSelected,
					nameof(ShoppingCartController),
					nameof(Minus));

				TempData["error"] = TempDataMessages.InvalidItem;
				return RedirectToAction(nameof(Index), "Home");
			}

			try
			{
				await _shoppingCartService.ReduceCountAsync(cartId);

				return RedirectToAction(nameof(Index));
			}
			catch (Exception ex)
			{
				TempData["error"] = ex.Message;

				return RedirectToAction(nameof(Index), "Home");
			}

		}

		[HttpGet]
		public async Task<IActionResult> Remove(int? cartId)
		{
			if (cartId == null)
			{
				_logger.LogError(LoggMessages.NullIdSelected,
					nameof(ShoppingCartController),
					nameof(Remove));

				TempData["error"] = TempDataMessages.InvalidItem;
				return RedirectToAction(nameof(Index), "Home");
			}

			try
			{
				await _shoppingCartService.RemoveItemAsync(cartId);

				return RedirectToAction(nameof(Index));
			}
			catch (Exception ex)
			{
				TempData["error"] = ex.Message;

				return RedirectToAction(nameof(Index), "Home");
			}

		}
	}
}
