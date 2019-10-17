using System.Collections.Generic;
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

        public IActionResult Create()
        {
            var professor = new Professor();

            professor.AtribuicaoCursos = new List<AtribuicaoCurso>();

            PopularDadosCursosAtribuidos(professor);

            return View();
        }

        // POST: Professores/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Sala,Sobrenome,Nome,DataContratacao")] Professor professor, string[] cursosSelecionados)
        {
            if (cursosSelecionados != null)
            {
                professor.AtribuicaoCursos = new List<AtribuicaoCurso>();
                foreach (var curso in cursosSelecionados)
                {
                    var cursoParaAdicionar = new AtribuicaoCurso { ProfessorId = professor.Id, CursoId = int.Parse(curso) };

                    professor.AtribuicaoCursos.Add(cursoParaAdicionar);
                }
            }

            if (ModelState.IsValid)
            {
                _context.Add(professor);

                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            PopularDadosCursosAtribuidos(professor);

            return View(professor);
        }

        // GET: Professores/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var professor = await _context.Professores
                .Include(i => i.Sala)
                .Include(i => i.AtribuicaoCursos).ThenInclude(i => i.Curso)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == id);

            if (professor == null)
                return NotFound();

            PopularDadosCursosAtribuidos(professor);

            return View(professor);
        }

        private void PopularDadosCursosAtribuidos(Professor professor)
        {
            var todosCursos = _context.Cursos;

            var cursosProfessor = new HashSet<int>(professor.AtribuicaoCursos.Select(c => c.CursoId));

            var viewModel = new List<DadosAtribuidosCurso>();

            foreach (var curso in todosCursos)
            {
                viewModel.Add(new DadosAtribuidosCurso()
                {
                    CursoId = curso.CursoId,
                    Nome = curso.Nome,
                    Atribuido = cursosProfessor.Contains(curso.CursoId)
                });
            }

            ViewData["Cursos"] = viewModel;
        }

        // POST: Professores/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, string[] cursosSelecionados)
        {
            if (id == null)
                return NotFound();

            var professorParaAtualizar = await _context.Professores
                .Include(i => i.Sala)
                .Include(i => i.AtribuicaoCursos)
                    .ThenInclude(i => i.Curso)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (!await TryUpdateModelAsync(
                professorParaAtualizar,
                "",
                p => p.Nome, p => p.Sobrenome, p => p.DataContratacao, p => p.Sala
            ))
            {
                AtualizarCursosProfessores(cursosSelecionados, professorParaAtualizar);

                PopularDadosCursosAtribuidos(professorParaAtualizar);

                return View(professorParaAtualizar);
            }


            if (string.IsNullOrWhiteSpace(professorParaAtualizar.Sala?.Localizacao))
                professorParaAtualizar.Sala = null;

            AtualizarCursosProfessores(cursosSelecionados, professorParaAtualizar);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.)
                ModelState.AddModelError("", "Unable to save changes. " +
                                             "Try again, and if the problem persists, " +
                                             "see your system administrator.");
            }
            return RedirectToAction(nameof(Index));

        }

        private void AtualizarCursosProfessores(string[] cursosSelecionados, Professor professorParaAtualizar)
        {
            if (cursosSelecionados == null)
            {
                professorParaAtualizar.AtribuicaoCursos = new List<AtribuicaoCurso>();

                return;
            }

            var cursoSelecionadosHs = new HashSet<string>(cursosSelecionados);

            var cursosProfessor = new HashSet<int>
                (professorParaAtualizar.AtribuicaoCursos.Select(c => c.Curso.CursoId));

            foreach (var curso in _context.Cursos)
            {
                if (cursoSelecionadosHs.Contains(curso.CursoId.ToString()))
                {
                    if (!cursosProfessor.Contains(curso.CursoId))
                        professorParaAtualizar.AtribuicaoCursos.Add(new AtribuicaoCurso { ProfessorId = professorParaAtualizar.Id, CursoId = curso.CursoId });
                }
                else
                {
                    if (!cursosProfessor.Contains(curso.CursoId)) continue;

                    var cursoParaRemover = professorParaAtualizar.AtribuicaoCursos.FirstOrDefault(i => i.CursoId == curso.CursoId);

                    if (cursoParaRemover != null) _context.Remove((object) cursoParaRemover);
                }
            }
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
            var professor = await _context.Professores
                .Include(i => i.AtribuicaoCursos)
                .SingleAsync(i => i.Id == id);

            var departamentos = await _context.Departamentos
                .Where(d => d.ProfessorId == id)
                .ToListAsync();

            departamentos.ForEach(d => d.ProfessorId = null);

            _context.Professores.Remove(professor);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
