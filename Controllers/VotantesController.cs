using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Final_IngenieriaSoftware.Models;

namespace Final_IngenieriaSoftware.Controllers
{
    public class VotantesController : Controller
    {
        private readonly SistemaDeVotacionContext _context;

        public VotantesController(SistemaDeVotacionContext context)
        {
            _context = context;
        }

        // GET: Votantes
        public async Task<IActionResult> Index()
        {
            var sistemaDeVotacionContext = _context.Votantes.Include(v => v.RolIdRolNavigation);
            return View(await sistemaDeVotacionContext.ToListAsync());
        }

        // GET: Votantes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Votantes == null)
            {
                return NotFound();
            }

            var votante = await _context.Votantes
                .Include(v => v.RolIdRolNavigation)
                .FirstOrDefaultAsync(m => m.Idvotante == id);
            if (votante == null)
            {
                return NotFound();
            }

            return View(votante);
        }

        // GET: Votantes/Create
        public IActionResult Create()
        {
            ViewData["RolIdRol"] = new SelectList(_context.Rols, "IdRol", "IdRol");
            return View();
        }

        // POST: Votantes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Idvotante,Dpi,Nombre,RolIdRol")] Votante votante)
        {
            var state = _context.StateApps;
            var validate = state.FirstOrDefault(x => x.IdstateApp.Equals(1));
            if (!(bool)validate.State)
            {
                if (ModelState.IsValid)
                {
                    _context.Add(votante);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            ViewData["RolIdRol"] = new SelectList(_context.Rols, "IdRol", "IdRol", votante.RolIdRol);
            return View(votante);
        }

        // GET: Votantes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Votantes == null)
            {
                return NotFound();
            }

            var votante = await _context.Votantes.FindAsync(id);
            if (votante == null)
            {
                return NotFound();
            }
            ViewData["RolIdRol"] = new SelectList(_context.Rols, "IdRol", "IdRol", votante.RolIdRol);
            return View(votante);
        }

        // POST: Votantes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Idvotante,Dpi,Nombre,RolIdRol")] Votante votante)
        {
            if (id != votante.Idvotante)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(votante);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VotanteExists(votante.Idvotante))
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
            ViewData["RolIdRol"] = new SelectList(_context.Rols, "IdRol", "IdRol", votante.RolIdRol);
            return View(votante);
        }

        // GET: Votantes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Votantes == null)
            {
                return NotFound();
            }

            var votante = await _context.Votantes
                .Include(v => v.RolIdRolNavigation)
                .FirstOrDefaultAsync(m => m.Idvotante == id);
            if (votante == null)
            {
                return NotFound();
            }

            return View(votante);
        }

        // POST: Votantes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Votantes == null)
            {
                return Problem("Entity set 'SistemaDeVotacionContext.Votantes'  is null.");
            }
            var votante = await _context.Votantes.FindAsync(id);
            if (votante != null)
            {
                _context.Votantes.Remove(votante);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VotanteExists(int id)
        {
          return (_context.Votantes?.Any(e => e.Idvotante == id)).GetValueOrDefault();
        }
    }
}
