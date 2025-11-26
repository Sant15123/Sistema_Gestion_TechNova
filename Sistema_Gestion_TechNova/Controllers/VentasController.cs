using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sistema_Gestion_TechNova.Models;
using Sistema_Gestion_TechNova.Services;

namespace Sistema_Gestion_TechNova.Controllers
{
    [Authorize(Roles = "Admin")]
    public class VentaController : Controller
    {
        private readonly IClienteService _clienteService;
        private readonly IProductoService _productoService;
        private readonly IVentaService _ventaService;

        public VentaController(
            IClienteService clienteService,
            IProductoService productoService,
            IVentaService ventaService)
        {
            _clienteService = clienteService;
            _productoService = productoService;
            _ventaService = ventaService;
        }

        // =============== LISTADO DE VENTAS ===============
        public async Task<IActionResult> Index(string cliente, DateTime? desde, DateTime? hasta)
        {
            var ventas = await _ventaService.GetAllAsync(cliente, desde, hasta);

            ViewBag.Cliente = cliente;
            ViewBag.Desde = desde?.ToString("yyyy-MM-dd");
            ViewBag.Hasta = hasta?.ToString("yyyy-MM-dd");

            return View(ventas);
        }

        // =============== REGISTRAR VENTA ===============
        public async Task<IActionResult> Create()
        {
            ViewBag.Clientes = await _clienteService.GetAllAsync();
            ViewBag.Productos = await _productoService.GetAllAsync();

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Venta venta)
        {
            if (venta.Items == null || !venta.Items.Any())
            {
                ModelState.AddModelError("", "Debe agregar al menos 1 producto a la venta.");
            }

            if (!ModelState.IsValid)
            {
                ViewBag.Clientes = await _clienteService.GetAllAsync();
                ViewBag.Productos = await _productoService.GetAllAsync();
                return View(venta);
            }

            venta.Fecha = DateTime.Now;

            // Calcular total
            venta.Total = venta.Items.Sum(i => i.Subtotal);

            var resultado = await _ventaService.RegistrarVentaAsync(venta);

            if (resultado != "OK")
            {
                ModelState.AddModelError("", resultado);
                ViewBag.Clientes = await _clienteService.GetAllAsync();
                ViewBag.Productos = await _productoService.GetAllAsync();
                return View(venta);
            }

            return RedirectToAction(nameof(Index));
        }

        // =============== DETALLES DE VENTA ===============
        public async Task<IActionResult> Details(string id)
        {
            var venta = await _ventaService.GetByIdAsync(id);
            if (venta == null)
                return NotFound();

            return View(venta);
        }
    }
}
