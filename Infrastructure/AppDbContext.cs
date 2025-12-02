using Domain;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Usuario> Usuarios => Set<Usuario>();
        public DbSet<Produto> Produtos => Set<Produto>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Relacionamento: Produto -> UsuarioCadastro
            modelBuilder.Entity<Produto>()
                .HasOne(p => p.UsuarioCadastro)
                .WithMany()
                .HasForeignKey(p => p.IdUsuarioCadastro)
                .OnDelete(DeleteBehavior.Restrict);

            // Relacionamento: Produto -> UsuarioUpdate
            modelBuilder.Entity<Produto>()
                .HasOne(p => p.UsuarioUpdate)
                .WithMany()
                .HasForeignKey(p => p.IdUsuarioUpdate)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
