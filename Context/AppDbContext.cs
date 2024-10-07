using ListaDeTarefas.Models;
using Microsoft.EntityFrameworkCore;

namespace ListaDeTarefas.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Tarefa> Tarefas { get; set; }
        public DbSet<Categoria> Caregoria { get; set; }
        public DbSet<Status> Status { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Categoria>().HasData(
                new Categoria { CategoriaId = "estudo", Nome = "Estudo" },
                new Categoria { CategoriaId = "trabalho", Nome = "Trabalho" },
                new Categoria { CategoriaId = "lazer", Nome = "Lazer" },
                new Categoria { CategoriaId = "casa", Nome = "Casa" }
            );

            modelBuilder.Entity<Status>().HasData(
                new Status { StatusId = "pendente", Nome = "Pendente" },
                new Status { StatusId = "concluido", Nome = "Concluído" }
            );
        }
    }
}
