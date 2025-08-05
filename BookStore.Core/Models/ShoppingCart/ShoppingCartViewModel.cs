namespace BookStore.Core.Models.ShoppingCart
{
	public class ShoppingCartViewModel
	{
		public IEnumerable<ShoppingCartServiceModel> ShoppingCartList { get; set; } 
			= new List<ShoppingCartServiceModel>();

		public decimal TotalPrice { get; set; }	
	}
}
