using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Roles_Estructuras_Control.Data;
using Roles_Estructuras_Control.Models;

namespace Roles_Estructuras_Control.Controllers
{
    [Authorize(Roles = "Admin,Escritor,Letor")]
    public class ProveedoresController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProveedoresController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Proveedores
        public async Task<IActionResult> Index()
        {
            return View(await _context.Proveedores.ToListAsync());
        }

        // GET: Proveedores/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var proveedoresModels = await _context.Proveedores
                .FirstOrDefaultAsync(m => m.Id == id);
            if (proveedoresModels == null)
            {
                return NotFound();
            }

            return View(proveedoresModels);
        }

        [Authorize(Roles = "Admin,Escritor")]
        // GET: Proveedores/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Proveedores/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin,Escritor")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NombreProveedor,Direccion,Telefono,Correo")] ProveedoresModels proveedoresModels)
        {
            if (ModelState.IsValid)
            {
                _context.Add(proveedoresModels);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(proveedoresModels);
        }
        [Authorize(Roles = "Admin,Escritor")]
        // GET: Proveedores/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var proveedoresModels = await _context.Proveedores.FindAsync(id);
            if (proveedoresModels == null)
            {
                return NotFound();
            }
            return View(proveedoresModels);
        }

        // POST: Proveedores/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin,Escritor")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NombreProveedor,Direccion,Telefono,Correo")] ProveedoresModels proveedoresModels)
        {
            if (id != proveedoresModels.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(proveedoresModels);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProveedoresModelsExists(proveedoresModels.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(proveedoresModels);
        }
        [Authorize(Roles = "Admin,Escritor")]
        // GET: Proveedores/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var proveedoresModels = await _context.Proveedores
                .FirstOrDefaultAsync(m => m.Id == id);
            if (proveedoresModels == null)
            {
                return NotFound();
            }

            return View(proveedoresModels);
        }
        [Authorize(Roles = "Admin,Escritor")]
        // POST: Proveedores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var proveedoresModels = await _context.Proveedores.FindAsync(id);
            if (proveedoresModels != null)
            {
                _context.Proveedores.Remove(proveedoresModels);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProveedoresModelsExists(int id)
        {
            return _context.Proveedores.Any(e => e.Id == id);
        }
    }
}
