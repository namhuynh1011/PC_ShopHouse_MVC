using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using PC_ShopHouse.Models;
using PC_ShopHouse.Repositories;
using PC_ShopHouse.ViewModels;

namespace PC_ShopHouse.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IBrandRepository _brandRepository;
        private readonly ICPURepository _cpuRepository;
        private readonly IMainboardRepository _mainboardRepository;
        public ProductController(IProductRepository productRepository,
       ICategoryRepository categoryRepository, IBrandRepository brandRepository, ICPURepository cpuRepository
            , IMainboardRepository mainboardRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _brandRepository = brandRepository;
            _cpuRepository = cpuRepository;
            _mainboardRepository = mainboardRepository;
        }
        public async Task<IActionResult> Index(int? categoryId)
        {
            var categories = await _categoryRepository.GetAllAsync();
            ViewBag.Categories = new SelectList(categories, "Id", "CategoryName");

            var products = await _productRepository.GetAllAsync(); // IEnumerable<Product>

            if (categoryId.HasValue)
            {
                products = products.Where(p => p.CategoryId == categoryId.Value);
            }

            return View(products);
        }

        //Them San Pham
        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var vm = new ProductCreateViewModel
            {
                Categories = await _categoryRepository.GetAllAsync(),
                Brands = await _brandRepository.GetAllAsync()
            };
            return View(vm);
        }
        [HttpPost]
        public async Task<IActionResult> Add(ProductCreateViewModel vm)
        {
            vm.Categories = await _categoryRepository.GetAllAsync();
            vm.Brands = await _brandRepository.GetAllAsync();

            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            // Xử lý ảnh
            if (vm.ImageFile != null)
            {
                vm.Product.ImageUrl = await SaveImage(vm.ImageFile); // SaveImage là hàm lưu ảnh trả về đường dẫn
            }

            await _productRepository.AddAsync(vm.Product);

            // Nếu là CPU, lưu thêm bảng CPU
            var category = vm.Categories.FirstOrDefault(c => c.Id == vm.Product.CategoryId);
            if (category != null && category.CategoryName == "CPU" && vm.CPU != null)
            {
                vm.CPU.ProductId = vm.Product.Id;
                await _cpuRepository.AddCPUAsync(vm.CPU);
            }

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Display(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null) return NotFound();

            var vm = new ProductDetailViewModel { Product = product };

            // Lấy chi tiết theo category
            var category = await _categoryRepository.GetByIdAsync(product.CategoryId);
            if (category?.CategoryName == "CPU")
                vm.CPU = await _cpuRepository.GetByProductIdAsync(product.Id);
            //else if (category?.CategoryName == "Mainboard")
            //    vm.Mainboard = await _mainboardRepository.GetByProductIdAsync(product.Id);
            // ... các loại khác

            return View(vm);
        }
        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null) return NotFound();

            var vm = new ProductEditViewModel
            {
                Product = product,
                Categories = (await _categoryRepository.GetAllAsync()).Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.CategoryName }),
                Brands = (await _brandRepository.GetAllAsync()).Select(b => new SelectListItem { Value = b.Id.ToString(), Text = b.BrandName }),
            };

            // Nạp dữ liệu chi tiết linh kiện
            var category = await _categoryRepository.GetByIdAsync(product.CategoryId);
            if (category?.CategoryName == "CPU")
                vm.CPU = await _cpuRepository.GetByProductIdAsync(product.Id);
            //else if (category?.CategoryName == "Mainboard")
            //    vm.Mainboard = await _mainboardRepository.GetByProductIdAsync(product.Id);
            // ... các loại khác

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Update(ProductEditViewModel vm, IFormFile? imageFile)
        {
            var product = await _productRepository.GetByIdAsync(vm.Product.Id);
            if (product == null) return NotFound();

            // Validate, giữ lại dropdown nếu lỗi
            if (!ModelState.IsValid)
            {
                vm.Categories = (await _categoryRepository.GetAllAsync()).Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.CategoryName });
                vm.Brands = (await _brandRepository.GetAllAsync()).Select(b => new SelectListItem { Value = b.Id.ToString(), Text = b.BrandName });
                return View(vm);
            }

            // Cập nhật Product
            product.ProductName = vm.Product.ProductName;
            product.Price = vm.Product.Price;
            product.Description = vm.Product.Description;
            product.CategoryId = vm.Product.CategoryId;
            product.BrandId = vm.Product.BrandId;

            // Xử lý ảnh
            if (imageFile != null && imageFile.Length > 0)
            {
                product.ImageUrl = await SaveImage(imageFile);
            }

            await _productRepository.UpdateAsync(product);

            // Cập nhật bảng chi tiết theo category
            var category = await _categoryRepository.GetByIdAsync(product.CategoryId);
            if (category?.CategoryName == "CPU" && vm.CPU != null)
            {
                vm.CPU.ProductId = product.Id;
                await _cpuRepository.UpdateOrCreateAsync(vm.CPU);
            }
            //else if (category?.CategoryName == "Mainboard" && vm.Mainboard != null)
            //{
            //    vm.Mainboard.ProductId = product.Id;
            //    await _mainboardRepository.UpdateOrCreateAsync(vm.Mainboard);
            //}
            // ... xử lý các loại khác

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }
        // Xử lý xóa sản phẩm
        [HttpPost, ActionName("DeleteConfirmed")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _productRepository.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }






        // Hàm Sử Lý Ngoài
        private async Task<string> SaveImage(IFormFile image)
        {
            var savePath = Path.Combine("wwwroot/images", image.FileName);
            using (var fileStream = new FileStream(savePath, FileMode.Create))
            {
                await image.CopyToAsync(fileStream);
            }
            return "/images/" + image.FileName;
        }
        private async Task PopulateSelectLists()
        {
            var categories = await _categoryRepository.GetAllAsync();
            ViewBag.Categories = new SelectList(categories, "Id", "CategoryName");
            var brands = await _brandRepository.GetAllAsync();
            ViewBag.Brands = new SelectList(brands, "Id", "BrandName");
        }

    }
}

