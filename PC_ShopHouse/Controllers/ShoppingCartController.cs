using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PC_ShopHouse.Models;
using PC_ShopHouse.Models.Extentions;
using PC_ShopHouse.Repositories;
using System.Security.Claims;
namespace PC_ShopHouse.Controllers
{
    public class ShoppingCartController : Controller
    {
        private IProductRepository _productRepository;
        private readonly ApplicationDbContext _context;

        public ShoppingCartController(IProductRepository productRepository, ApplicationDbContext context)
        {
            _productRepository = productRepository;
            _context = context;
        }

        public IActionResult Index()
        {
            var cart = HttpContext.Session.GetObjectFromSession<ShoppingCart>("Cart") ?? new ShoppingCart();
            return View(cart);
        }

        public async Task<IActionResult> AddItemToCart(int productId, int quantity)
        {
            var _product = await _productRepository.GetByIdAsync(productId);
            if (_product != null)
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

            return Ok();
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

        [HttpGet]
        public IActionResult Checkout()
        {
            var cart = HttpContext.Session.GetObjectFromSession<ShoppingCart>("Cart") ?? new ShoppingCart();
            if (cart.Items.Count == 0)
            {
                TempData["Error"] = "Giỏ hàng của bạn đang trống!";
                return RedirectToAction("Index");
            }
            var user = User;
            var name = user.Identity.Name;
            return View(new CheckoutViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Checkout(CheckoutViewModel model)
        {
            var cart = HttpContext.Session.GetObjectFromSession<ShoppingCart>("Cart") ?? new ShoppingCart();
            if (cart.Items.Count == 0)
            {
                TempData["Error"] = "Giỏ hàng của bạn đang trống!";
                return RedirectToAction("Index");
            }
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // Lấy userId từ Identity
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // Tạo Order mới
            var order = new Order
            {
                UserId = userId,
                CustomerName = model.Name,
                Address = model.Address,
                Email = model.Email,
                Phone = model.Phone,
                OrderDate = DateTime.Now,
                Items = cart.Items.Select(i => new OrderItem
                {
                    ProductId = i.ProductId,
                    ProductName = i.ProductName,
                    Price = i.Price,
                    Quantity = i.Quantity
                }).ToList()
            };

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            // Xoá giỏ hàng
            HttpContext.Session.Remove("Cart");

            TempData["Success"] = "Đặt hàng thành công!";

            return RedirectToAction("MyOrders");
        }
        
        [HttpGet]
        public async Task<IActionResult> MyOrders()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var orders = await _context.Orders
                .Include(o => o.Items)
                .Where(o => o.UserId == userId)
                .OrderByDescending(o => o.OrderDate)
                .ToListAsync();
            return View(orders);
        }
    }
}
