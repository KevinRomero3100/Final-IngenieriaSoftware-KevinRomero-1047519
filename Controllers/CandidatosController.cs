using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Final_IngenieriaSoftware.Models;
using System.Net;

namespace Final_IngenieriaSoftware.Controllers
{
    public class CandidatosController : Controller
    {
        private readonly SistemaDeVotacionContext _context;

        public CandidatosController(SistemaDeVotacionContext context)
        {
            _context = context;
        }

        // GET: Candidatos
        public async Task<IActionResult> Index()
        {
              return _context.Candidatos != null ? 
                          View(await _context.Candidatos.ToListAsync()) :
                          Problem("Entity set 'SistemaDeVotacionContext.Candidatos'  is null.");
        }

        // GET: Candidatos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Candidatos == null)
            {
                return NotFound();
            }

            var candidato = await _context.Candidatos
                .FirstOrDefaultAsync(m => m.Idcandidatos == id);
            if (candidato == null)
            {
                return NotFound();
            }

            return View(candidato);
        }

        // GET: Candidatos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Candidatos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Idcandidatos,Nombre,Partido,Afiliados")] Candidato candidato)
        {
            var state = _context.StateApps;
            var validate = state.FirstOrDefault(x => x.IdstateApp.Equals(1));
            if ((bool)validate.State)
            {
                if (ModelState.IsValid)
                {
                    _context.Add(candidato);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            var response = new HttpResponseMessage(HttpStatusCode.BadRequest);
            return BadRequest();
        }

        // GET: Candidatos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Candidatos == null)
            {
                return NotFound();
            }

            var candidato = await _context.Candidatos.FindAsync(id);
            if (candidato == null)
            {
                return NotFound();
            }
            return View(candidato);
        }

        // POST: Candidatos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Idcandidatos,Nombre,Partido,Afiliados")] Candidato candidato)
        {
            if (id != candidato.Idcandidatos)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(candidato);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CandidatoExists(candidato.Idcandidatos))
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
            return View(candidato);
        }

        // GET: Candidatos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Candidatos == null)
            {
                return NotFound();
            }

            var candidato = await _context.Candidatos
                .FirstOrDefaultAsync(m => m.Idcandidatos == id);
            if (candidato == null)
            {
                return NotFound();
            }

            return View(candidato);
        }

        // POST: Candidatos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Candidatos == null)
            {
                return Problem("Entity set 'SistemaDeVotacionContext.Candidatos'  is null.");
            }
            var candidato = await _context.Candidatos.FindAsync(id);
            if (candidato != null)
            {
                _context.Candidatos.Remove(candidato);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CerrarInscriopciones(int estado)
        {
            StateApp change = new StateApp();
            change.State = true;
            change.IdstateApp = 1;
            _context.Update(change);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));

        }
        private bool CandidatoExists(int id)
        {
          return (_context.Candidatos?.Any(e => e.Idcandidatos == id)).GetValueOrDefault();
        }
    }
}
