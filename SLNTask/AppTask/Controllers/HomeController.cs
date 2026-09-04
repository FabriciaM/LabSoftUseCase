using System.Diagnostics;
using AppTask.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AppTask.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DbTasksContext _context;

        public HomeController(ILogger<HomeController> logger, DbTasksContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            // "Pendente" é o valor usado pelo formulário atual; "Em Aberto"
            // também é aceito para contemplar registros antigos.
            ViewBag.TarefasEmAberto = await _context.Tarefas.CountAsync(t =>
                t.StatusTarefa.ToLower() == "pendente" ||
                t.StatusTarefa.ToLower() == "em aberto");
            ViewBag.TarefasEmAndamento = await _context.Tarefas.CountAsync(t =>
                t.StatusTarefa.ToLower() == "em andamento");
            ViewBag.TarefasConcluidas = await _context.Tarefas.CountAsync(t =>
                t.StatusTarefa.ToLower() == "concluído" ||
                t.StatusTarefa.ToLower() == "concluido" ||
                t.StatusTarefa.ToLower() == "concluída" ||
                t.StatusTarefa.ToLower() == "concluida");
            // No cadastro de Incidente, "Sim" significa resolvido.
            ViewBag.IncidentesAtivos = await _context.Incidentes.CountAsync(i =>
                i.Resolvido.ToLower() != "sim" &&
                i.Resolvido.ToLower() != "true" &&
                i.Resolvido != "1");

            ViewBag.TarefasRecentes = await _context.Tarefas
                .AsNoTracking()
                .Include(t => t.Funcionario)
                .OrderByDescending(t => t.Codigo)
                .Take(5)
                .ToListAsync();

            var proximosPrazos = await _context.Tarefas
                .AsNoTracking()
                .Include(t => t.Funcionario)
                .Where(t => t.DataPlanejada >= DateTime.Now)
                .OrderBy(t => t.DataPlanejada)
                .Take(3)
                .ToListAsync();

            // Se não houver tarefas futuras, exibe as próximas cadastradas para
            // que o painel não fique vazio.
            if (proximosPrazos.Count == 0)
            {
                proximosPrazos = await _context.Tarefas
                    .AsNoTracking()
                    .Include(t => t.Funcionario)
                    .OrderByDescending(t => t.DataPlanejada)
                    .Take(3)
                    .ToListAsync();
            }

            ViewBag.ProximosPrazos = proximosPrazos;

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
