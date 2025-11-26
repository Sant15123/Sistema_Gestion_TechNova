using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sistema_Gestion_TechNova.Models;
using Sistema_Gestion_TechNova.Services;

namespace Sistema_Gestion_TechNova.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ProductoController : Controller
    {
        private readonly IProductoService _productoService;

        public ProductoController(IProductoService productoService)
        {
            _productoService = productoService;
        }

        public async Task<IActionResult> Index(string search)
        {
            var productos = await _productoService.GetAllAsync(search);
            ViewBag.Search = search;
            return View(productos);
        }

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(Producto producto)
        {
            if (!ModelState.IsValid)
                return View(producto);

            // Validación: Código único
            if (await _productoService.CodigoExisteAsync(producto.Codigo))
            {
                ModelState.AddModelError("Codigo", "Este código ya está registrado.");
                return View(producto);
            }

            await _productoService.CreateAsync(producto);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(string id)
        {
            var product = await _productoService.GetByIdAsync(id);
            if (product == null)
                return NotFound();

            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Producto producto)
        {
            if (!ModelState.IsValid)
                return View(producto);

            // Validación: Código único excepto el actual
            if (await _productoService.CodigoExisteAsync(producto.Codigo, producto.Id.ToString()))
            {
                ModelState.AddModelError("Codigo", "Este código ya está registrado en otro producto.");
                return View(producto);
            }

            await _productoService.UpdateAsync(producto);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(string id)
        {
            var product = await _productoService.GetByIdAsync(id);
            if (product == null)
                return NotFound();

            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            await _productoService.DeleteAsync(id, id);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(string id)
        {
            var product = await _productoService.GetByIdAsync(id);
            if (product == null)
                return NotFound();

            return View(product);
        }
    }
}
