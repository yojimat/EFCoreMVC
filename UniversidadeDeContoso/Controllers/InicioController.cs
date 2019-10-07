using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UniversidadeDeContoso.Dados;
using UniversidadeDeContoso.Models;
using UniversidadeDeContoso.Models.UniversidadeViewModels;

namespace UniversidadeDeContoso.Controllers
{
    public class InicioController : Controller
    {
        private readonly UniversidadeContext _contexto;

        public InicioController(UniversidadeContext contexto) => _contexto = contexto;

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacidade()
        {
            return View();
        }

        public async Task<ActionResult> Sobre()
        {
            var dados =
                from estudandes in _contexto.Estudantes
                group estudandes by estudandes.DataDeMatricula into grupoDeData
                select new GrupoDataDeMatricula
                {
                    DataDeMatricula = grupoDeData.Key,
                    QuantidadeDeEstudantes = grupoDeData.Count()
                };

            return View(await dados.AsNoTracking().ToListAsync());
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() => View(new ErrorViewModel
                {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
    }
}
