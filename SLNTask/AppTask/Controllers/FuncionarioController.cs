using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AppTask.Models;

namespace AppTask.Controllers
{
    public class FuncionarioController : Controller
    {
        private readonly DbTasksContext _context;

        public FuncionarioController(DbTasksContext context)
        {
            _context = context;
        }

        // GET: Funcionario
        public async Task<IActionResult> Index()
        {
            return View(await _context.Funcionarios.Include(f => f.Gerente).AsNoTracking().ToListAsync());
        }

        // GET: Funcionario/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var funcionario = await _context.Funcionarios
                .Include(f => f.Gerente)
                .FirstOrDefaultAsync(m => m.Codigo == id);
            if (funcionario == null)
            {
                return NotFound();
            }

            return View(funcionario);
        }

        // GET: Funcionario/Create
        public IActionResult Create()
        {
            CarregarGerentes();
            return View();
        }

        // POST: Funcionario/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nome,Cargo,GerenteId")] Funcionario funcionario)
        {
            if (funcionario.CodigoGerente.HasValue && funcionario.CodigoGerente == funcionario.Codigo)
                ModelState.AddModelError(nameof(Funcionario.CodigoGerente), "O funcionário não pode ser seu próprio gerente.");

            if (ModelState.IsValid)
            {
                _context.Add(funcionario);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            CarregarGerentes(funcionario.CodigoGerente);
            return View(funcionario);
        }

        // GET: Funcionario/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var funcionario = await _context.Funcionarios.FindAsync(id);
            if (funcionario == null)
            {
                return NotFound();
            }
            CarregarGerentes(funcionario.CodigoGerente, funcionario.Codigo);
            return View(funcionario);
        }

        // POST: Funcionario/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Codigo,Nome,Cargo,GerenteId")] Funcionario funcionario)
        {
            if (id != funcionario.Codigo)
            {
                return NotFound();
            }

            if (funcionario.CodigoGerente.HasValue && funcionario.CodigoGerente == funcionario.Codigo)
                ModelState.AddModelError(nameof(Funcionario.CodigoGerente), "O funcionário não pode ser seu próprio gerente.");

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(funcionario);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FuncionarioExists(funcionario.Codigo))
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
            CarregarGerentes(funcionario.CodigoGerente, funcionario.Codigo);
            return View(funcionario);
        }

        // GET: Funcionario/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var funcionario = await _context.Funcionarios
                .FirstOrDefaultAsync(m => m.Codigo == id);
            if (funcionario == null)
            {
                return NotFound();
            }

            return View(funcionario);
        }

        // POST: Funcionario/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var funcionario = await _context.Funcionarios.FindAsync(id);
            if (funcionario != null)
            {
                _context.Funcionarios.Remove(funcionario);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private void CarregarGerentes(int? selecionado = null, int? ignorar = null)
        {
            var gerentes = _context.Funcionarios.AsNoTracking().AsQueryable();
            if (ignorar.HasValue)
                gerentes = gerentes.Where(f => f.Codigo != ignorar.Value);

            ViewData["ListaGerentes"] = new SelectList(
                gerentes.OrderBy(f => f.Nome), "Codigo", "Nome", selecionado);
        }

        private bool FuncionarioExists(int id)
        {
            return _context.Funcionarios.Any(e => e.Codigo == id);
        }
    }
}
