using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using UniversidadeDeContoso.Dados;
using UniversidadeDeContoso.Models;

namespace UniversidadeDeContoso.Controllers
{
    public class CursosController : Controller
    {
        private readonly UniversidadeContext _context;

        public CursosController(UniversidadeContext context) => _context = context;

        // GET: Cursos
        public async Task<IActionResult> Index()
        {
            var cursos = _context.Cursos
                .Include(c => c.Departamento)
                .AsNoTracking();

            return View(await cursos.ToListAsync());
        }

        // GET: Cursos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var curso = await _context.Cursos
                .Include(c => c.Departamento)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.CursoId == id);

            if (curso == null)
                return NotFound();

            return View(curso);
        }

        public IActionResult Create()
        {
            PopularListaDropdownDepartamento();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CursoId,Creditos,DepartamentoId,Nome")] Curso curso)
        {
            if (ModelState.IsValid)
            {
                _context.Add(curso);

                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            PopularListaDropdownDepartamento(curso.DepartamentoId);

            return View(curso);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var curso = await _context.Cursos
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.CursoId == id);

            if (curso == null)
                return NotFound();
            
            PopularListaDropdownDepartamento(curso.DepartamentoId);

            return View(curso);
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(int? id)
        {
            if (id == null)
                return NotFound();

            var cursoParaAtualizar = await _context.Cursos
                .FirstOrDefaultAsync(c => c.CursoId == id);

            if (await TryUpdateModelAsync<Curso>(cursoParaAtualizar,
                "",
                c => c.Creditos, c => c.DepartamentoId, c => c.Nome))
            {
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException ex )
                {
                    //Log the error (uncomment ex variable name and write a log.)
                    ModelState.AddModelError("", "Unable to save changes. " +
                                                 "Try again, and if the problem persists, " +
                                                 $"see your system administrator. {ex}");
                }
                return RedirectToAction(nameof(Index));
            }

            PopularListaDropdownDepartamento(cursoParaAtualizar.DepartamentoId);

            return View(cursoParaAtualizar);
        }

        private void PopularListaDropdownDepartamento(object departamentoSelecionado = null)
        {
            var queryDeDepartamentos = from d in _context.Departamentos
                orderby d.Nome
                select d;

            ViewBag.DepartamentoId = new SelectList(queryDeDepartamentos.AsNoTracking(), "DepartamentoId", "Nome", departamentoSelecionado);
        }

        // GET: Cursos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var curso = await _context.Cursos
                .Include(c => c.Departamento)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.CursoId == id);

            if (curso == null)
                return NotFound();

            return View(curso);
        }

        // POST: Cursos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var curso = await _context.Cursos.FindAsync(id);
            _context.Cursos.Remove(curso);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
