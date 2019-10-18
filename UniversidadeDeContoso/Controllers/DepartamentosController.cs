using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using UniversidadeDeContoso.Dados;
using UniversidadeDeContoso.Models;

namespace UniversidadeDeContoso.Controllers
{
    public class DepartamentosController : Controller
    {
        private readonly UniversidadeContext _context;

        public DepartamentosController(UniversidadeContext context) => _context = context;

        // GET: Departamentos
        public async Task<IActionResult> Index()
        {
            var universidadeContext = _context.Departamentos.Include(d => d.Administrador);

            return View(await universidadeContext.ToListAsync());
        }

        // GET: Departamentos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var departamento = await _context.Departamentos
                .Include(d => d.Administrador)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.DepartamentoId == id);

            if (departamento == null)
                return NotFound();

            return View(departamento);
        }

        // GET: Departamentos/Create
        public IActionResult Create()
        {
            ViewData["ProfessorId"] = new SelectList(_context.Professores, "Id", "NomeCompleto");
            return View();
        }

        // POST: Departamentos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DepartamentoId,Nome,Orcamento,DataComeco,ProfessorId,VersaoFileira")] Departamento departamento)
        {
            if (ModelState.IsValid)
            {
                _context.Add(departamento);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProfessorId"] = new SelectList(_context.Professores, "Id", "NomeCompleto", departamento.ProfessorId);
            return View(departamento);
        }

        // GET: Departamentos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var departamento = await _context.Departamentos
                .Include(i => i.Administrador)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.DepartamentoId == id);

            if (departamento == null)
                return NotFound();

            ViewData["ProfessorId"] = new SelectList(_context.Professores, "Id", "NomeCompleto", departamento.ProfessorId);

            return View(departamento);
        }

        // POST: Departamentos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, byte[] versaoFileira)
        {
            if (id == null)
                return NotFound();

            var departamentoParaAtualizar = await _context.Departamentos
                .Include(d => d.Administrador)
                .FirstOrDefaultAsync(d => d.DepartamentoId == id);

            if (departamentoParaAtualizar == null)
            {
                var departamentoDeletado = new Departamento();

                await TryUpdateModelAsync(departamentoDeletado);

                ModelState.AddModelError(string.Empty,
                    "Unable to save changes. The department was deleted by another user.");

                ViewData["InstructorID"] = new SelectList(_context.Professores, "ID", "FullName", departamentoDeletado.ProfessorId);

                return View(departamentoDeletado);
            }

            _context.Entry(departamentoParaAtualizar).Property("VersaoFileira").OriginalValue = versaoFileira;

            if (await TryUpdateModelAsync(
                departamentoParaAtualizar,
                "",
                s => s.Nome, s => s.DataComeco, s => s.Orcamento, s => s.ProfessorId))
            {
                try
                {
                    await _context.SaveChangesAsync();

                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    var entradaDaExcessao = ex.Entries.Single();

                    var valoresCliente = (Departamento)entradaDaExcessao.Entity;

                    var entradaBancoDeDados = entradaDaExcessao.GetDatabaseValues();

                    if (entradaBancoDeDados == null)
                        ModelState.AddModelError(string.Empty,
                            "Unable to save changes. The department was deleted by another user.");
                    else
                    {
                        var valoresBancoDeDados = (Departamento)entradaBancoDeDados.ToObject();

                        if (valoresBancoDeDados.Nome != valoresCliente.Nome)
                            ModelState.AddModelError("Nome", $"Current value: {valoresBancoDeDados.Nome}");

                        if (valoresBancoDeDados.Orcamento != valoresCliente.Orcamento)
                            ModelState.AddModelError("Orcamento", $"Current value: {valoresBancoDeDados.Orcamento:c}");

                        if (valoresBancoDeDados.DataComeco != valoresCliente.DataComeco)
                            ModelState.AddModelError("DataComeco", $"Current value: {valoresBancoDeDados.DataComeco:d}");

                        if (valoresBancoDeDados.ProfessorId != valoresCliente.ProfessorId)
                        {
                            var databaseInstructor =
                                await _context.Professores.FirstOrDefaultAsync(i =>
                                    i.Id == valoresBancoDeDados.ProfessorId);

                            ModelState.AddModelError("ProfessorId", $"Current value: {databaseInstructor?.NomeCompleto}");
                        }

                        ModelState.AddModelError(string.Empty, "The record you attempted to edit "
                                                               + "was modified by another user after you got the original value. The "
                                                               + "edit operation was canceled and the current values in the database "
                                                               + "have been displayed. If you still want to edit this record, click "
                                                               + "the Save button again. Otherwise click the Back to List hyperlink.");

                        departamentoParaAtualizar.VersaoFileira = valoresBancoDeDados.VersaoFileira;

                        ModelState.Remove("VersaoFileira");
                    }
                }
            }

            ViewData["ProfessorId"] = new SelectList(_context.Professores, "Id", "NomeCompleto", departamentoParaAtualizar.ProfessorId);

            return View(departamentoParaAtualizar);
        }

        // GET: Departamentos/Delete/5
        public async Task<IActionResult> Delete(int? id, bool? erroConcorrencia)
        {
            if (id == null)
                return NotFound();

            var departamento = await _context.Departamentos
                .Include(d => d.Administrador)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.DepartamentoId == id);

            if (departamento == null)
            {
                if (erroConcorrencia.GetValueOrDefault())
                    return RedirectToAction(nameof(Index));

                return NotFound();
            }

            if (erroConcorrencia.GetValueOrDefault())
                ViewData["MensagemErroConcorrencia"] = "The record you attempted to delete "
                                                      + "was modified by another user after you got the original values. "
                                                      + "The delete operation was canceled and the current values in the "
                                                      + "database have been displayed. If you still want to delete this "
                                                      + "record, click the Delete button again. Otherwise "
                                                      + "click the Back to List hyperlink.";

            return View(departamento);
        }

        // POST: Departamentos/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Departamento departamento)
        {
            try
            {
                if (!await _context.Departamentos.AnyAsync(m => m.DepartamentoId == departamento.DepartamentoId))
                    return RedirectToAction(nameof(Index));

                _context.Departamentos.Remove(departamento);

                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateConcurrencyException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.)
                return RedirectToAction(nameof(Delete), new { erroConcorrencia = true, id = departamento.DepartamentoId });
            }
        }
    }
}
