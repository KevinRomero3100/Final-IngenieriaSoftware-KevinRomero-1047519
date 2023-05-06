using Final_IngenieriaSoftware.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Security.Policy;

namespace Final_IngenieriaSoftware.Controllers
{
    public class Login : Controller
    {

        private readonly SistemaDeVotacionContext _context;

        public Login(SistemaDeVotacionContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> LoginApp(string _dpi)
        {
            if (ModelState.IsValid)
            {
                // Verifica si el usuario y la contraseña son válidos
                bool response = await ValidateUser(_dpi);
                if (response)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "El nombre de usuario o la contraseña son incorrectos.");
                }
            }
            ViewData["RolIdRol"] = _dpi;
            // Si llegamos hasta aquí, algo falló, vuelve a mostrar la vista de inicio de sesión con el modelo actualizado
            return View("Index");
        }

        private async Task<bool> ValidateUser(string _dpi)
        {
            var votantes = _context.Votantes.Include(v => v.RolIdRolNavigation);
            if (votantes != null)
            {
                var votante = votantes.FirstOrDefault(x => x.Dpi.ToString().Equals(_dpi));
                if (votante != null)
                {
                    SingeltonUsuario.Instance.votante = votante;
                    return true;
                }
            }

            return false;
        }
    }
}
