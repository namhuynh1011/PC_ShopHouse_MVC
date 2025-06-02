using Microsoft.AspNetCore.Mvc;
using PC_ShopHouse.Models;
using PC_ShopHouse.Repositories;
using System.Threading.Tasks;

namespace PC_ShopHouse.Controllers
{
    public class BrandController : Controller
    {
        private readonly IBrandRepository _brandRepository;
        public BrandController(IBrandRepository brandRepository)
        {
            _brandRepository = brandRepository;
        }
        public async Task<IActionResult> Index()
        {
            var brands = await _brandRepository.GetAllAsync();
            return View(brands);
        }

        public async Task<IActionResult> Add()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(Brand brand)
        {
            if (ModelState.IsValid)
            {
                await _brandRepository.AddAsync(brand);
                return RedirectToAction(nameof(Index));
            }
            // Nếu có lỗi nhập, hiển thị lại Index với danh sách
            return View(brand);
        }

        public async Task<IActionResult> Update(int id)
        {
            var brand = await _brandRepository.GetByIdAsync(id);
            if (brand == null)
            {
                return NotFound();
            }
            return View(brand);
        }
        [HttpPost]
        public async Task<IActionResult> Update(int id, Brand brand)
        {
            if (id != brand.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                var existingBrand = await _brandRepository.GetByIdAsync(id);
                existingBrand.BrandName = brand.BrandName;
                await _brandRepository.UpdateAsync(existingBrand);
                return RedirectToAction(nameof(Index));
            }
            return View(brand);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _brandRepository.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
