using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UniversidadeDeContoso.Dados;
using UniversidadeDeContoso.Models;
using UniversidadeDeContoso.Models.UniversidadeViewModels;

namespace UniversidadeDeContoso.Controllers
{
    public class ProfessoresController : Controller
    {
        private readonly UniversidadeContext _context;

        public ProfessoresController(UniversidadeContext context) => _context = context;

        // GET: Professores
        public async Task<IActionResult> Index(int? id, int? cursoId)
        {
            var viewModel = new IndexDadosProfessor
            {
                Professores = await _context.Professores
                    .Include(i => i.Sala)
                    .Include(i => i.AtribuicaoCursos)
                    .ThenInclude(i => i.Curso)
                    .ThenInclude(i => i.Materias)
                    .ThenInclude(i => i.Estudante)
                    .Include(i => i.AtribuicaoCursos)
                    .ThenInclude(i => i.Curso)
                    .ThenInclude(i => i.Departamento)
                    .AsNoTracking()
                    .OrderBy(i => i.Sobrenome)
                    .ToListAsync()
            };


            if (id != null)
            {
                ViewData["ProfessorId"] = id.Value;

                Professor professor = viewModel.Professores?.Single(i => i.Id == id.Value);

                viewModel.Cursos = professor?.AtribuicaoCursos.Select(s => s.Curso);
            }

            if (cursoId != null)
            {
                ViewData["CursoId"] = cursoId.Value;

                viewModel.Materias = viewModel.Cursos?.Single(x => x.CursoId == cursoId).Materias;
            }

            return View(viewModel);
        }

        // GET: Professores/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var professor = await _context.Professores
                .FirstOrDefaultAsync(m => m.Id == id);
            if (professor == null)
            {
                return NotFound();
            }

            return View(professor);
        }

        // GET: Professores/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Professores/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Sobrenome,Nome,DataContratacao")] Professor professor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(professor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(professor);
        }

        // GET: Professores/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var professor = await _context.Professores.FindAsync(id);
            if (professor == null)
            {
                return NotFound();
            }
            return View(professor);
        }

        // POST: Professores/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Sobrenome,Nome,DataContratacao")] Professor professor)
        {
            if (id != professor.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(professor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProfessorExists(professor.Id))
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
            return View(professor);
        }

        // GET: Professores/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var professor = await _context.Professores
                .FirstOrDefaultAsync(m => m.Id == id);
            if (professor == null)
            {
                return NotFound();
            }

            return View(professor);
        }

        // POST: Professores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var professor = await _context.Professores.FindAsync(id);
            _context.Professores.Remove(professor);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProfessorExists(int id)
        {
            return _context.Professores.Any(e => e.Id == id);
        }
    }
}
