using System;
using System.Linq;
using UniversidadeDeContoso.Models;

namespace UniversidadeDeContoso.Dados
{
    public static class InicializadorBancoDeDados
    {
        public static void Inicializador(UniversidadeContext context)
        {
            context.Database.EnsureCreated();

            // Procura por estudantes.
            if (context.Estudantes.Any())
            {
                return;   // Banco ja foi populado
            }

            var estudantes = new Estudante[]
            {
                new Estudante{Nome="Carson",SobreNome="Alexander",DataDeMatricula=DateTime.Parse("2005-09-01")},
                new Estudante{Nome="Meredith",SobreNome="Alonso",DataDeMatricula=DateTime.Parse("2002-09-01")},
                new Estudante{Nome="Arturo",SobreNome="Anand",DataDeMatricula=DateTime.Parse("2003-09-01")},
                new Estudante{Nome="Gytis",SobreNome="Barzdukas",DataDeMatricula=DateTime.Parse("2002-09-01")},
                new Estudante{Nome="Yan",SobreNome="Li",DataDeMatricula=DateTime.Parse("2002-09-01")},
                new Estudante{Nome="Peggy",SobreNome="Justice",DataDeMatricula=DateTime.Parse("2001-09-01")},
                new Estudante{Nome="Laura",SobreNome="Norman",DataDeMatricula=DateTime.Parse("2003-09-01")},
                new Estudante{Nome="Nino",SobreNome="Olivetto",DataDeMatricula=DateTime.Parse("2005-09-01")}
            };

            foreach (Estudante s in estudantes)
            {
                context.Estudantes.Add(s);
            }

            context.SaveChanges();

            var cursos = new Curso[]
            {
                new Curso{CursoID=1050,Nome="Chemistry",Creditos=3},
                new Curso{CursoID=4022,Nome="Microeconomics",Creditos=3},
                new Curso{CursoID=4041,Nome="Macroeconomics",Creditos=3},
                new Curso{CursoID=1045,Nome="Calculus",Creditos=4},
                new Curso{CursoID=3141,Nome="Trigonometry",Creditos=4},
                new Curso{CursoID=2021,Nome="Composition",Creditos=3},
                new Curso{CursoID=2042,Nome="Literature",Creditos=4}
            };

            foreach (Curso c in cursos)
            {
                context.Cursos.Add(c);
            }

            context.SaveChanges();

            var materias = new Materia[]
            {
                new Materia{EstudanteID=1,CursoID=1050,Nota=Nota.A},
                new Materia{EstudanteID=1,CursoID=4022,Nota=Nota.C},
                new Materia{EstudanteID=1,CursoID=4041,Nota=Nota.B},
                new Materia{EstudanteID=2,CursoID=1045,Nota=Nota.B},
                new Materia{EstudanteID=2,CursoID=3141,Nota=Nota.F},
                new Materia{EstudanteID=2,CursoID=2021,Nota=Nota.F},
                new Materia{EstudanteID=3,CursoID=1050},
                new Materia{EstudanteID=4,CursoID=1050},
                new Materia{EstudanteID=4,CursoID=4022,Nota=Nota.F},
                new Materia{EstudanteID=5,CursoID=4041,Nota=Nota.C},
                new Materia{EstudanteID=6,CursoID=1045},
                new Materia{EstudanteID=7,CursoID=3141,Nota=Nota.A}
            };

            foreach (Materia e in materias)
            {
                context.Materias.Add(e);
            }

            context.SaveChanges();
        }
    }
}
