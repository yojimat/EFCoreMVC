using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UniversidadeDeContoso.Dados;
using UniversidadeDeContoso.Models;

namespace UniversidadeDeContoso.Controllers
{
    public class EstudantesController : Controller
    {
        private readonly UniversidadeContext _context;

        public EstudantesController(UniversidadeContext context)
        {
            _context = context;
        }

        // GET: Estudantes
        public async Task<IActionResult> Index()
        {
            return View(await _context.Estudantes.ToListAsync());
        }

        // GET: Estudantes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var estudante = await _context.Estudantes
                .Include(e => e.Materias)
                .ThenInclude(m => m.Curso)
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.ID == id);

            if (estudante == null)
            {
                return NotFound();
            }

            return View(estudante);
        }

        // GET: Estudantes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Estudantes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SobreNome,Nome,DataDeMatricula")] Estudante estudante)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(estudante);

                    await _context.SaveChangesAsync();

                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateException error)
            {
                ModelState.AddModelError("", "Não foi possivel salvar os dados." +
                    $"Tente de novo, e se o problema persistir contate o administrador do sistema, error:{error}");
            } 
            return View(estudante);
        }

        // GET: Estudantes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var estudante = await _context.Estudantes.FindAsync(id);
            if (estudante == null)
            {
                return NotFound();
            }
            return View(estudante);
        }

        // POST: Estudantes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(int? id)
        {
            if (id == null)
                return NotFound();

            var estudandoParaEdicao = await _context.Estudantes.FirstOrDefaultAsync(e => e.ID == id);

            if (await TryUpdateModelAsync(estudandoParaEdicao, "", e => e.Nome, e => e.SobreNome, e => e.DataDeMatricula))
            {
                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException error)
                {
                    ModelState.AddModelError("", "Não foi possivel salvar os dados." +
                        $"Tente de novo, e se o problema persistir contate o administrador do sistema, error:{error}");
                }
            }
            return View(estudandoParaEdicao);
        }

        // GET: Estudantes/Delete/5
        public async Task<IActionResult> Delete(int? id, bool? mudancasAoSalvar = false)
        {
            if (id == null)
                return NotFound();

            var estudante = await _context.Estudantes
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.ID == id);

            if (estudante == null)
                return NotFound();

            if (mudancasAoSalvar.GetValueOrDefault())
                ViewData["MensagemDeErro"] =
                    "Exclusão do estudante falhou. Tente novamente, e se o problema persistir " +
                    "contate o administrador do seu sistema.";

            return View(estudante);
        }

        // POST: Estudantes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmado(int id)
        {
            var estudante = await _context.Estudantes
                .FindAsync(id);

            if (estudante == null)
                return RedirectToAction(nameof(Index));

            try
            {
                _context.Estudantes.Remove(estudante);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.)
                return RedirectToAction(nameof(Delete), new { id, saveChangesError = true });
            }
        }

        private bool EstudanteExists(int id)
        {
            return _context.Estudantes.Any(e => e.ID == id);
        }
    }
}
