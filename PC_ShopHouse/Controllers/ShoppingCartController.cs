using Microsoft.AspNetCore.Mvc;
using PC_ShopHouse.Repositories;
using PC_ShopHouse.Models.Extentions;
using PC_ShopHouse.Models;
namespace PC_ShopHouse.Controllers
{
    public class ShoppingCartController : Controller
    {
        private IProductRepository _productRepository;

        public ShoppingCartController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public IActionResult Index()
        {
            var cart = HttpContext.Session.GetObjectFromSession<ShoppingCart>("Cart") ?? new ShoppingCart();
            return View();
        }

        public async Task<IActionResult> AddItemToCart(int productId, int quantity)
        {
            var _product = await _productRepository.GetByIdAsync(productId);
            if (_product == null)
            {
                CartItem cartIteem = new CartItem()
                {
                    ProductId = productId,
                    ProductName = _product.ProductName,
                    Price = _product.Price,
                    Quantity = quantity

                };
                var cart = HttpContext.Session.GetObjectFromSession<ShoppingCart>("Cart") ?? new ShoppingCart();
                cart.AddItem(cartIteem);
                HttpContext.Session.SetOjectAsJson("Cart", cart);
            }

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> RemoveItemFromCart(int productId)
        {
            var cart = HttpContext.Session.GetObjectFromSession<ShoppingCart>("Cart");
           
            if (cart is not null)
            {
                cart.RemoveItem(productId);
                HttpContext.Session.SetOjectAsJson("Cart", cart);
            }
            return RedirectToAction("Index");

        }
    }
}
