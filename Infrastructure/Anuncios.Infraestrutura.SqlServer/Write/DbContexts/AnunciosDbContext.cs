using Anuncios.Domain.Entities;
using Anuncios.Infrastructure.SqlServer.Write.Mappings;
using Microsoft.EntityFrameworkCore;

namespace Anuncios.Infrastructure.SqlServer.Write.DbContexts
{
    public class AnunciosDbContext : DbContext
    {
        private readonly string _connectionString;

        public AnunciosDbContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                 .UseLazyLoadingProxies()
                 .UseSqlServer(_connectionString);
        }

        public virtual DbSet<Anuncio> Anuncios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.ApplyConfiguration(new AnuncioMap());
            base.OnModelCreating(modelBuilder);
        }
    }
}
