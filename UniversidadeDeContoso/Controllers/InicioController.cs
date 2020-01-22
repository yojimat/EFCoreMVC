using System.Collections.Generic;
using System.Data.Common;
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
            //Código para ultima parte do tutorial
            List<GrupoDataDeMatricula> grupos = new List<GrupoDataDeMatricula>();

            var conn = _contexto.Database.GetDbConnection();

            try
            {
                await conn.OpenAsync();

                using (var comando = conn.CreateCommand())
                {
                    string query = "SELECT DataDeMatricula, COUNT(*) AS QuantidadeDeEstudantes "
                        + "FROM Pessoa "
                        + "WHERE Discriminator = 'Estudante'"
                        + "GROUP BY DataDeMatricula";

                    comando.CommandText = query;
                    DbDataReader leitor = await comando.ExecuteReaderAsync();

                    if (leitor.HasRows)
                        while (await leitor.ReadAsync())
                        {
                            var linha = new GrupoDataDeMatricula
                            {
                                DataDeMatricula = leitor.GetDateTime(0),
                                QuantidadeDeEstudantes = leitor.GetInt32(1)
                            };
                            grupos.Add(linha);
                        }

                    leitor.Dispose();
                }
            }
            finally
            {
                conn.Close();
            }

            return View(grupos);

            //var dados =
            //    from estudandes in _contexto.Estudantes
            //    group estudandes by estudandes.DataDeMatricula into grupoDeData
            //    select new GrupoDataDeMatricula
            //    {
            //        DataDeMatricula = grupoDeData.Key,
            //        QuantidadeDeEstudantes = grupoDeData.Count()
            //    };

            //return View(await dados.AsNoTracking().ToListAsync());
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() => View(new ErrorViewModel
                {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
    }
}
