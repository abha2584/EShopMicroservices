
namespace Shopping.Web.Pages
{
    public class CheckoutModel(IBasketService basketService, ILogger<CheckoutModel> logger) : PageModel
    {

        [BindProperty]
        public BasketCheckoutModel Order { get; set; } = new BasketCheckoutModel();

        public ShoppingCartModel Cart { get; set; } = new ShoppingCartModel();

		public async Task<IActionResult> OnGetAsync()
        {

            Cart = await basketService.LoadUserBasket();
            return Page();
		}

        public async Task<IActionResult> OnPostCheckoutAsync()
        {
            logger.LogInformation("Checkout button clicked");
            Cart = await basketService.LoadUserBasket();
            Order.CustomerId = new Guid("b3f1c8e2-5d6a-4f9e-9c1a-2b3e4f5d6a7b");
            Order.UserName = Cart.UserName;
			Order.TotalPrice = Cart.TotalPrice;
            await basketService.CheckoutBasket(new CheckoutBasketRequest(Order));
            return RedirectToPage("Confirmation" , "OrderSubmitted");
		}
	}
}
