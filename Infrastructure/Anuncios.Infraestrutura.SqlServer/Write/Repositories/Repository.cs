using Anuncios.Infrastructure.SqlServer.Write.DbContexts;
using Microsoft.EntityFrameworkCore;
using SharedKernel.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Anuncios.Infrastructure.SqlServer.Write.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly AnunciosDbContext Db;
        protected readonly DbSet<TEntity> DbSet;

        public Repository(AnunciosDbContext context)
        {
            Db = context;
            DbSet = Db.Set<TEntity>();
        }

        public virtual void Adicionar(TEntity obj)
        {
            DbSet.Add(obj);
        }

        public virtual async Task<TEntity> ObterPeloIdAsync(int id)
        {
            return await DbSet.FindAsync(id);
        }

        public virtual void Alterar(TEntity obj)
        {
            DbSet.Update(obj);
        }

        public virtual void Excluir(int id)
        {
            DbSet.Remove(DbSet.Find(id));
        }

        public async Task<int> SalvarAsync()
        {
            return await Db.SaveChangesAsync();
        }

        public void Dispose()
        {
            Db.Dispose();
            GC.SuppressFinalize(this);
        }

        public void AdicionarVarios(IEnumerable<TEntity> obj)
        {
            DbSet.AddRange(obj);
        }

        public void ExcluirVarios(IEnumerable<TEntity> obj)
        {
            DbSet.RemoveRange(obj);
        }

        public async Task<bool> ExisteAsync(int id)
        {
            return await DbSet.FindAsync(id) != null;
        }
    }
}
