using appReverso.Models;
using appReversotask.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace appReverso.Controllers
{
    public class AccountController : Controller
    {
        private readonly DbClinicaContext _context;

        public AccountController(DbClinicaContext context)
        {
            _context = context;
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var paciente = await _context.Pacientes
                .FirstOrDefaultAsync(p => p.Cpf == model.Cpf);

            if (paciente == null)
            {
                ModelState.AddModelError("", "CPF não encontrado.");
                return View(model);
            }

            // Remove qualquer sessão anterior
            HttpContext.Session.Clear();

            // Remove qualquer cookie de autenticação anterior
            await HttpContext.SignOutAsync(
                CookieAuthenticationDefaults.AuthenticationScheme);

            // Cria a nova sessão
            HttpContext.Session.SetInt32("PacienteId", paciente.Codigo);
            HttpContext.Session.SetString("PacienteNome", paciente.Nome);

            // Cria o login do novo paciente
            var claims = new List<Claim>
            {
                new Claim(
                    ClaimTypes.NameIdentifier,
                    paciente.Codigo.ToString()),

                new Claim(
                    ClaimTypes.Name,
                    paciente.Nome)
            };

            var identity = new ClaimsIdentity(
                claims,
                CookieAuthenticationDefaults.AuthenticationScheme);

            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                principal);

            return RedirectToAction("Index", "Consulta");
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            // Apaga os dados do paciente anterior
            HttpContext.Session.Clear();

            // Apaga o cookie de autenticação
            await HttpContext.SignOutAsync(
                CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction(nameof(Login));
        }
    }
}
