using ListaDeTarefas.Context;
using ListaDeTarefas.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ListaDeTarefas.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;
        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Adicionar()
        {
            ViewBag.Categorias = _context.Caregoria.ToList();
            ViewBag.Status = _context.Status.ToList();

            var tarefa = new Tarefa { StatusId = "pendente" };

            return View(tarefa);
        }

        public IActionResult Index(string filtroString)
        {
            var filtros = new Filtro(filtroString);

            ViewBag.Filtros = filtros;
            ViewBag.Categorias = _context.Caregoria.ToList();
            ViewBag.Status = _context.Status.ToList();
            ViewBag.Vencimento = Filtro.FiltroVencimentos;

            IQueryable<Tarefa> consulta = _context.Tarefas;

            if (filtros.TemCategoria)
            {
                consulta = consulta.Where(tarefa => tarefa.CategoriaId == filtros.CategoriaId);
            }

            if (filtros.TemStatus)
            {
                consulta = consulta.Where(tarefa => tarefa.StatusId == filtros.StatusId);
            }

            if (filtros.TemVencimento)
            {
                var hoje = DateTime.Today;

                if(filtros.Passado)
                {
                    consulta = consulta.Where(tarefa => tarefa.DataVencimento < hoje);
                }

                if (filtros.Futuro)
                {
                    consulta = consulta.Where(tarefa => tarefa.DataVencimento > hoje);
                }

                if (filtros.Hoje)
                {
                    consulta = consulta.Where(tarefa => tarefa.DataVencimento == hoje);
                }
            }


            var tarefas = consulta.OrderBy(tarefa => tarefa.DataVencimento).ToList();


            return View(tarefas);
        }

        [HttpPost]
        public IActionResult Filtrar(string[] filtro)
        {
            return RedirectToAction("Index", new { filtroString = string.Join("-", filtro) });
        }

        [HttpPost]
        public IActionResult MarcarCompleto([FromRoute] string id, Tarefa tarefaSelecionada)
        {
            tarefaSelecionada = _context.Tarefas.Find(tarefaSelecionada.Id);

            if (tarefaSelecionada != null)
            {
                tarefaSelecionada.StatusId = "concluido";
                _context.SaveChanges();
            }


            return RedirectToAction("Index", new { filtroString = id });
        }

        [HttpPost]
        public IActionResult Adicionar(Tarefa tarefa)
        {
            if (ModelState.IsValid)
            {
                _context.Tarefas.Add(tarefa);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }

            ViewBag.Categorias = _context.Caregoria.ToList();
            ViewBag.Status = _context.Status.ToList();

            return View("Adicionar", tarefa);
        }

        [HttpPost]
        public IActionResult DeletarCompletos(string id)
        {
            var paraDeletar = _context.Tarefas.Where(s => s.StatusId == "concluido").ToList();

            foreach (var tarefa in paraDeletar)
            {
                _context.Tarefas.Remove(tarefa);
            }
                _context.SaveChanges();
            return RedirectToAction("Index", new {ID = id});
        }   
               
    }
}
