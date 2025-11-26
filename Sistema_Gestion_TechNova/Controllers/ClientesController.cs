using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sistema_Gestion_TechNova.Models;
using Sistema_Gestion_TechNova.Services;

namespace Sistema_Gestion_TechNova.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ClienteController : Controller
    {
        private readonly IClienteService _clienteService;

        public ClienteController(IClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        public async Task<IActionResult> Index(string search)
        {
            var clientes = await _clienteService.GetAllAsync(search);
            ViewBag.Search = search;
            return View(clientes);
        }

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(Cliente cliente)
        {
            if (!ModelState.IsValid)
                return View(cliente);

            await _clienteService.CreateAsync(cliente);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(string id)
        {
            var cliente = await _clienteService.GetByIdAsync(id);
            if (cliente == null) return NotFound();

            return View(cliente);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Cliente cliente)
        {
            if (!ModelState.IsValid)
                return View(cliente);

            await _clienteService.UpdateAsync(cliente);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(string id)
        {
            var cliente = await _clienteService.GetByIdAsync(id);
            if (cliente == null) return NotFound();

            return View(cliente);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            await _clienteService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(string id)
        {
            var cliente = await _clienteService.GetByIdAsync(id);
            if (cliente == null) return NotFound();

            return View(cliente);
        }
    }
}
