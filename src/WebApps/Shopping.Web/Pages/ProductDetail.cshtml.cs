

using Shopping.Web.Services;

namespace Shopping.Web.Pages
{
    public class ProductDetailModel(IBasketService basketService,ICatalogService catalogService,ILogger<ProductDetailModel> logger) 
        : PageModel
    {

        public ProductModel Product { get; set; } = new ProductModel();

        [BindProperty]
        public string Color { get; set; } = string.Empty;

        [BindProperty]
        public int Quantity { get; set; } = default!;
		public async Task<IActionResult> OnGetAsync(Guid productId)
        {
            var response = await catalogService.GetProductById(productId);
            Product = response.Product;
			return Page();
        }

		public async Task<IActionResult> OnPostAddToCartAsync(Guid productId)
		{
			logger.LogInformation("Add to cart button clicked");
			var productResponse = await catalogService.GetProductById(productId);
			var basket = await basketService.LoadUserBasket();
			basket.Items.Add(new ShoppingCartItemModel
			{
				ProductId = productId,
				ProductName = productResponse.Product.Name,
				Price = productResponse.Product.Price,
				Quantity = 1,
				Color = "Black"
			});
			await basketService.StoreBasket(new StoreBasketRequest(basket));
			return RedirectToPage("Cart");
		}
	}
}
