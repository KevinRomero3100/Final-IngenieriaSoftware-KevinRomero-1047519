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
    public class VotacionesController : Controller
    {
        private readonly SistemaDeVotacionContext _context;

        public VotacionesController(SistemaDeVotacionContext context)
        {
            _context = context;
        }

        // GET: Votaciones
        public async Task<IActionResult> Index()
        {
            var sistemaDeVotacionContext = _context.Votaciones.Include(v => v.CandidatosIdcandidatosNavigation).Include(v => v.VotanteIdvotanteNavigation);
            return View(await sistemaDeVotacionContext.ToListAsync());
        }

        // GET: Votaciones/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Votaciones == null)
            {
                return NotFound();
            }

            var votacione = await _context.Votaciones
                .Include(v => v.CandidatosIdcandidatosNavigation)
                .Include(v => v.VotanteIdvotanteNavigation)
                .FirstOrDefaultAsync(m => m.IdnewTable == id);
            if (votacione == null)
            {
                return NotFound();
            }

            return View(votacione);
        }

        // GET: Votaciones/Create
        public IActionResult Create()
        {
            var state = _context.StateApps;
            var validate = state.FirstOrDefault(x => x.IdstateApp.Equals(1));
            if (!(bool)validate.State)
            {
                ViewData["CandidatosIdcandidatos"] = new SelectList(_context.Candidatos, "Idcandidatos", "Idcandidatos");
                ViewData["VotanteIdvotante"] = new SelectList(_context.Votantes, "Idvotante", "Idvotante");
                return View();
            }
            return BadRequest();
        }

        // POST: Votaciones/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdnewTable,VotanteIdvotante,CandidatosIdcandidatos")] Votacione votacione)
        {
            if (ModelState.IsValid)
            {
                _context.Add(votacione);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CandidatosIdcandidatos"] = new SelectList(_context.Candidatos, "Idcandidatos", "Idcandidatos", votacione.CandidatosIdcandidatos);
            ViewData["VotanteIdvotante"] = new SelectList(_context.Votantes, "Idvotante", "Idvotante", votacione.VotanteIdvotante);
            return BadRequest();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CerrarVotacion(int estado)
        {
           StateApp change = new StateApp();
            change.State = false;
            change.IdstateApp= 1;
                    _context.Update(change);
                    await _context.SaveChangesAsync();
              
                return RedirectToAction(nameof(Index));

        }

        private bool VotacioneExists(int id)
        {
          return (_context.Votaciones?.Any(e => e.IdnewTable == id)).GetValueOrDefault();
        }
    }
}
