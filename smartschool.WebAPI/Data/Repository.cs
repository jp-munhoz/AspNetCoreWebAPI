using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using smartschool.WebAPI.Models;

namespace smartschool.WebAPI.Data
{
    public class Repository : IRepository
    {
        private readonly DataContext context;

        public Repository(DataContext context)
        {
            this.context = context;
        }
        public void Add<T>(T entity) where T : class
        {
            context.Add(entity);
        }
        public void Update<T>(T entity) where T : class
        {
            context.Update(entity);
        }
        public void Delete<T>(T entity) where T : class
        {
            context.Remove(entity);
        }
        public bool SaveChanges()
        {
            return (context.SaveChanges() > 0);
        }

        public Aluno[] GetAllAlunos(bool includeProfessor = false)
        {
            IQueryable<Aluno> query = context.Alunos;

            if (includeProfessor)
            {
                query = query.Include(ad => ad.AlunosDisciplinas)
                             .ThenInclude(d => d.Disciplina)
                             .ThenInclude(p => p.Professor);
            }

            query = query.AsNoTracking()
                         .OrderBy(aluno => aluno.Id);

            return query.ToArray();
        }

        public Aluno[] GetAllAlunosByDisciplinaId(int disciplinaId, bool includeProfessor = false)
        {
            IQueryable<Aluno> query = context.Alunos;

            if (includeProfessor)
            {
                query = query.Include(ad => ad.AlunosDisciplinas)
                             .ThenInclude(d => d.Disciplina)
                             .ThenInclude(p => p.Professor);
            }

            query = query.AsNoTracking()
                         .OrderBy(a => a.Id)
                         .Where(b => b.AlunosDisciplinas.Any(ad => ad.DisciplinaId == disciplinaId));

            return query.ToArray();
        }

        public Aluno GetAlunoById(int alunoId, bool includeProfessor = false)
        {
            IQueryable<Aluno> query = context.Alunos;

            if (includeProfessor)
            {
                query = query.Include(ad => ad.AlunosDisciplinas)
                             .ThenInclude(d => d.Disciplina)
                             .ThenInclude(p => p.Professor);
            }

            query = query.AsNoTracking()
                         .OrderBy(aluno => aluno.Id).Where(a => a.Id == alunoId);

            return query.FirstOrDefault();

        }

        public Professor[] GetAllProfessores(bool includeAlunos = false)
        {
            IQueryable<Professor> query = context.Professores;

            if (includeAlunos)
            {
                query = query.Include(d => d.Disciplinas)
                             .ThenInclude(ad => ad.AlunosDisciplinas)
                             .ThenInclude(a => a.Aluno);
            }

            query = query.AsNoTracking()
                         .OrderBy(p => p.Id);

            return query.ToArray();
        }

        public Professor[] GetAllProfessoresByDisciplinaId(int disciplinaId, bool includeAlunos = false)
        {
            IQueryable<Professor> query = context.Professores;

            if (includeAlunos)
            {
                query = query.Include(d => d.Disciplinas)
                             .ThenInclude(ad => ad.AlunosDisciplinas)
                             .ThenInclude(a => a.Aluno);
            }

            query = query.AsNoTracking()
                         .OrderBy(aluno => aluno.Id)
                         .Where(aluno => aluno.Disciplinas.Any(d => d.AlunosDisciplinas.Any(ad => ad.DisciplinaId == disciplinaId)));

            return query.ToArray();
        }

        public Professor GetProfessorById(int professorId, bool includeAlunos = false)
        {
            IQueryable<Professor> query = context.Professores;

            if (includeAlunos)
            {

                query = query.Include(a => a.Disciplinas)
                             .ThenInclude(b => b.AlunosDisciplinas)
                             .ThenInclude(c => c.Aluno);
            }

            query = query.AsNoTracking().OrderBy(prof => prof.Id).Where(profe => profe.Id == professorId);

            return query.FirstOrDefault();
        }
    }
}