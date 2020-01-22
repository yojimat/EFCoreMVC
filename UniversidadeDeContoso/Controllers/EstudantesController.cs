using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using UniversidadeDeContoso.Dados;
using UniversidadeDeContoso.Models;

namespace UniversidadeDeContoso.Controllers
{
    public class EstudantesController : Controller
    {
        private readonly UniversidadeContext _context;

        public EstudantesController(UniversidadeContext context) => _context = context;

        // GET: Estudantes
        public async Task<IActionResult> Index(
            string tipoDeOrdenamento, 
            string textoProcurado,
            string filtroSelecionado,
            int? numeroDaPagina
        )
        {
            ViewData["OrdenamentoSelecionado"] = tipoDeOrdenamento;
            ViewData["parametroNome"] = string.IsNullOrEmpty(tipoDeOrdenamento) ? "nome_desc" : "";
            ViewData["parametroData"] = tipoDeOrdenamento == "Data" ? "data_desc" : "Data";


            if (textoProcurado != null)
                numeroDaPagina = 1;
            else
                textoProcurado = filtroSelecionado;

            ViewData["FiltroSelecionado"] = textoProcurado;

            var estudantes = from e in _context.Estudantes
                             select e;

            if (!string.IsNullOrEmpty(textoProcurado))
                estudantes = estudantes.Where(e => e.Sobrenome.Contains(textoProcurado) || e.Nome.Contains(textoProcurado));

            if (string.IsNullOrEmpty(tipoDeOrdenamento))
                tipoDeOrdenamento = "Sobrenome";

            bool decrescente = false;

            if (tipoDeOrdenamento.EndsWith("_desc"))
            {
                tipoDeOrdenamento = tipoDeOrdenamento.Substring(0, tipoDeOrdenamento.Length - 5);
                decrescente = true;
            }

            if (decrescente)
                estudantes = estudantes.OrderByDescending(e => EF.Property<object>(e, tipoDeOrdenamento));
            else
                estudantes = estudantes.OrderBy(e => EF.Property<object>(e, tipoDeOrdenamento));

            //switch (tipoDeOrdenamento)
            //{
            //    case "nome_desc":
            //        estudantes = estudantes.OrderByDescending(s => s.Nome);
            //        break;
            //    case "Data":
            //        estudantes = estudantes.OrderBy(s => s.DataDeMatricula);
            //        break;
            //    case "data_desc":
            //        estudantes = estudantes.OrderByDescending(s => s.DataDeMatricula);
            //        break;
            //    default:
            //        estudantes = estudantes.OrderBy(s => s.Sobrenome);
            //        break;
            //}

            int tamanhoDaPagina = 3;

            return View(await ListaComPaginas<Estudante>.CreateAsync(estudantes.AsNoTracking(), numeroDaPagina ?? 1, tamanhoDaPagina));
        }

        // GET: Estudantes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var estudante = await _context.Estudantes
                .Include(e => e.Materias)
                .ThenInclude(m => m.Curso)
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.Id == id);

            if (estudante == null)
                return NotFound();

            return View(estudante);
        }

        // GET: Estudantes/Create
        public IActionResult Create() => View();

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
                return NotFound();

            var estudante = await _context.Estudantes.FindAsync(id);

            if (estudante == null)
                return NotFound();

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

            var estudandoParaEdicao = await _context.Estudantes.FirstOrDefaultAsync(e => e.Id == id);

            if (await TryUpdateModelAsync(estudandoParaEdicao, "", e => e.Nome, e => e.Sobrenome, e => e.DataDeMatricula))
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
                .FirstOrDefaultAsync(e => e.Id == id);

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
    }
}
