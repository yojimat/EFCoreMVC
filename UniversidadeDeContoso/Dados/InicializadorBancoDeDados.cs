using System;
using System.Linq;
using UniversidadeDeContoso.Models;

namespace UniversidadeDeContoso.Dados
{
    public static class InicializadorBancoDeDados
    {
        public static void Inicializador(UniversidadeContext context)
        {
            //context.Database.EnsureCreated();

            // Procura por estudantes.
            if (context.Estudantes.Any())
            {
                return;   // Banco ja foi populado
            }

            var estudantes = new []
            {
                new Estudante{Nome="Carson",Sobrenome="Alexander",DataDeMatricula=DateTime.Parse("2005-09-01")},
                new Estudante{Nome="Meredith",Sobrenome="Alonso",DataDeMatricula=DateTime.Parse("2002-09-01")},
                new Estudante{Nome="Arturo",Sobrenome="Anand",DataDeMatricula=DateTime.Parse("2003-09-01")},
                new Estudante{Nome="Gytis",Sobrenome="Barzdukas",DataDeMatricula=DateTime.Parse("2002-09-01")},
                new Estudante{Nome="Yan",Sobrenome="Li",DataDeMatricula=DateTime.Parse("2002-09-01")},
                new Estudante{Nome="Peggy",Sobrenome="Justice",DataDeMatricula=DateTime.Parse("2001-09-01")},
                new Estudante{Nome="Laura",Sobrenome="Norman",DataDeMatricula=DateTime.Parse("2003-09-01")},
                new Estudante{Nome="Nino",Sobrenome="Olivetto",DataDeMatricula=DateTime.Parse("2005-09-01")}
            };

            foreach (Estudante s in estudantes)
            {
                context.Estudantes.Add(s);
            }

            context.SaveChanges();

            var professores = new []
            {
                new Professor { Nome = "Kim", Sobrenome = "Abercrombie",
                    DataContratacao = DateTime.Parse("1995-03-11") },
                new Professor { Nome = "Fadi", Sobrenome = "Fakhouri",
                    DataContratacao = DateTime.Parse("2002-07-06") },
                new Professor { Nome = "Roger", Sobrenome = "Harui",
                    DataContratacao = DateTime.Parse("1998-07-01") },
                new Professor { Nome = "Candace", Sobrenome = "Kapoor",
                    DataContratacao = DateTime.Parse("2001-01-15") },
                new Professor { Nome = "Roger", Sobrenome = "Zheng",
                    DataContratacao = DateTime.Parse("2004-02-12") }
            };

            foreach (Professor i in professores)
            {
                context.Professores.Add(i);
            }

            context.SaveChanges();

            var departamentos = new []
            {
                new Departamento { Nome = "English", Orcamento = 350000,
                    DataComeco = DateTime.Parse("2007-09-01"),
                    ProfessorId  = professores.Single( i => i.Sobrenome == "Abercrombie").Id },
                new Departamento { Nome = "Mathematics", Orcamento = 100000,
                    DataComeco = DateTime.Parse("2007-09-01"),
                    ProfessorId  = professores.Single( i => i.Sobrenome == "Fakhouri").Id },
                new Departamento { Nome = "Engineering", Orcamento = 350000,
                    DataComeco = DateTime.Parse("2007-09-01"),
                    ProfessorId  = professores.Single( i => i.Sobrenome == "Harui").Id },
                new Departamento { Nome = "Economics",   Orcamento = 100000,
                    DataComeco = DateTime.Parse("2007-09-01"),
                    ProfessorId  = professores.Single( i => i.Sobrenome == "Kapoor").Id }
            };

            foreach (Departamento d in departamentos)
            {
                context.Departamentos.Add(d);
            }

            context.SaveChanges();

            var cursos = new[]
            {
                new Curso{CursoId=1050,Nome="Chemistry",Creditos=3, DepartamentoId = departamentos.Single(d => d.Nome == "Engineering").DepartamentoId},
                new Curso{CursoId=4022,Nome="Microeconomics",Creditos=3, DepartamentoId = departamentos.Single(d => d.Nome == "Economics").DepartamentoId},
                new Curso{CursoId=4041,Nome="Macroeconomics",Creditos=3, DepartamentoId = departamentos.Single(d => d.Nome == "Economics").DepartamentoId},
                new Curso{CursoId=1045,Nome="Calculus",Creditos=4, DepartamentoId = departamentos.Single(d => d.Nome == "Mathematics").DepartamentoId},
                new Curso{CursoId=3141,Nome="Trigonometry",Creditos=4, DepartamentoId = departamentos.Single(d => d.Nome == "Mathematics").DepartamentoId},
                new Curso{CursoId=2021,Nome="Composition",Creditos=3, DepartamentoId = departamentos.Single(d => d.Nome == "English").DepartamentoId},
                new Curso{CursoId=2042,Nome="Literature",Creditos=4, DepartamentoId = departamentos.Single(d => d.Nome == "English").DepartamentoId}
            };

            foreach (Curso c in cursos)
            {
                context.Cursos.Add(c);
            }

            context.SaveChanges();

            var atribucaoSalas = new AtribuicaoSala[]
            {
                new AtribuicaoSala {
                    ProfessorId = professores.Single( i => i.Sobrenome == "Fakhouri").Id,
                    Localizacao = "Smith 17" },
                new AtribuicaoSala {
                    ProfessorId = professores.Single( i => i.Sobrenome == "Harui").Id,
                    Localizacao = "Gowan 27" },
                new AtribuicaoSala {
                    ProfessorId = professores.Single( i => i.Sobrenome == "Kapoor").Id,
                    Localizacao = "Thompson 304" },
            };

            foreach (AtribuicaoSala o in atribucaoSalas)
            {
                context.AtribuicaoSalas.Add(o);
            }

            context.SaveChanges();

            var atribuicaoCursos = new AtribuicaoCurso[]
            {
                new AtribuicaoCurso {
                    CursoId = cursos.Single(c => c.Nome == "Chemistry" ).CursoId,
                    ProfessorId = professores.Single(i => i.Sobrenome == "Kapoor").Id
                    },
                new AtribuicaoCurso {
                    CursoId = cursos.Single(c => c.Nome == "Chemistry" ).CursoId,
                    ProfessorId = professores.Single(i => i.Sobrenome == "Harui").Id
                    },
                new AtribuicaoCurso {
                    CursoId = cursos.Single(c => c.Nome == "Microeconomics" ).CursoId,
                    ProfessorId = professores.Single(i => i.Sobrenome == "Zheng").Id
                    },
                new AtribuicaoCurso {
                    CursoId = cursos.Single(c => c.Nome == "Macroeconomics" ).CursoId,
                    ProfessorId = professores.Single(i => i.Sobrenome == "Zheng").Id
                    },
                new AtribuicaoCurso {
                    CursoId = cursos.Single(c => c.Nome == "Calculus" ).CursoId,
                    ProfessorId = professores.Single(i => i.Sobrenome == "Fakhouri").Id
                    },
                new AtribuicaoCurso {
                    CursoId = cursos.Single(c => c.Nome == "Trigonometry" ).CursoId,
                    ProfessorId = professores.Single(i => i.Sobrenome == "Harui").Id
                    },
                new AtribuicaoCurso {
                    CursoId = cursos.Single(c => c.Nome == "Composition" ).CursoId,
                    ProfessorId = professores.Single(i => i.Sobrenome == "Abercrombie").Id
                    },
                new AtribuicaoCurso {
                    CursoId = cursos.Single(c => c.Nome == "Literature" ).CursoId,
                    ProfessorId = professores.Single(i => i.Sobrenome == "Abercrombie").Id
                    },
            };

            foreach (AtribuicaoCurso ci in atribuicaoCursos)
            {
                context.AtribuicaoCursos.Add(ci);
            }

            context.SaveChanges();

            var materias = new Materia[]
            {
                    new Materia {
                        EstudanteId = estudantes.Single(s => s.Sobrenome == "Alexander").Id,
                        CursoId = cursos.Single(c => c.Nome == "Chemistry" ).CursoId,
                        Nota = Nota.A
                    },
                    new Materia {
                        EstudanteId = estudantes.Single(s => s.Sobrenome == "Alexander").Id,
                        CursoId = cursos.Single(c => c.Nome == "Microeconomics" ).CursoId,
                        Nota = Nota.C
                    },
                    new Materia {
                        EstudanteId = estudantes.Single(s => s.Sobrenome == "Alexander").Id,
                        CursoId = cursos.Single(c => c.Nome == "Macroeconomics" ).CursoId,
                        Nota = Nota.B
                    },
                    new Materia {
                        EstudanteId = estudantes.Single(s => s.Sobrenome == "Alonso").Id,
                        CursoId = cursos.Single(c => c.Nome == "Calculus" ).CursoId,
                        Nota = Nota.B
                    },
                    new Materia {
                        EstudanteId = estudantes.Single(s => s.Sobrenome == "Alonso").Id,
                        CursoId = cursos.Single(c => c.Nome == "Trigonometry" ).CursoId,
                        Nota = Nota.B
                    },
                    new Materia {
                        EstudanteId = estudantes.Single(s => s.Sobrenome == "Alonso").Id,
                        CursoId = cursos.Single(c => c.Nome == "Composition" ).CursoId,
                        Nota = Nota.B
                    },
                    new Materia {
                        EstudanteId = estudantes.Single(s => s.Sobrenome == "Anand").Id,
                        CursoId = cursos.Single(c => c.Nome == "Chemistry" ).CursoId
                    },
                    new Materia {
                        EstudanteId = estudantes.Single(s => s.Sobrenome == "Anand").Id,
                        CursoId = cursos.Single(c => c.Nome == "Microeconomics").CursoId,
                        Nota = Nota.B
                    },
                new Materia {
                        EstudanteId = estudantes.Single(s => s.Sobrenome == "Barzdukas").Id,
                        CursoId = cursos.Single(c => c.Nome == "Chemistry").CursoId,
                        Nota = Nota.B
                    },
                    new Materia {
                        EstudanteId = estudantes.Single(s => s.Sobrenome == "Li").Id,
                        CursoId = cursos.Single(c => c.Nome == "Composition").CursoId,
                        Nota = Nota.B
                    },
                    new Materia {
                        EstudanteId = estudantes.Single(s => s.Sobrenome == "Justice").Id,
                        CursoId = cursos.Single(c => c.Nome == "Literature").CursoId,
                        Nota = Nota.B
                    }
            };

            foreach (Materia e in materias)
            {
                var materiasNoBanco = context.Materias.Where(
                    s =>
                        s.Estudante.Id == e.EstudanteId &&
                        s.Curso.CursoId == e.CursoId).SingleOrDefault();
                if (materiasNoBanco == null)
                {
                    context.Materias.Add(e);
                }
            }

            context.SaveChanges();
        }
    }
}
