using Microsoft.EntityFrameworkCore;
using ToDo.Models;

namespace ToDo.Data
{
    public class AtividadesDbContext : DbContext
    {
        public AtividadesDbContext(DbContextOptions<AtividadesDbContext> options)
            : base(options)
        {
        }

        public DbSet<Atividade> Atividades { get; set; }
        public DbSet<ListaAtividades<Atividade>> ListasAtividades { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ListaAtividades<Atividade>>().HasKey(l => l.Id);
            modelBuilder.Entity<Atividade>().HasKey(a => a.Id);
        }
    }

}
