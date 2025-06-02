using gs.Models;
using Microsoft.EntityFrameworkCore;

namespace gs.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Abrigo> Abrigos { get; set; }
        public DbSet<Alerta> Alertas { get; set; }
        public DbSet<RotaSegura> RotasSeguras { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Usuario 1:N Alerta
            modelBuilder.Entity<Usuario>()
                .HasMany(u => u.Alertas)
                .WithOne(a => a.Usuario)
                .HasForeignKey(a => a.UsuarioId)
                .OnDelete(DeleteBehavior.Cascade);

            // Abrigo 1:N RotaSegura
            modelBuilder.Entity<Abrigo>()
                .HasMany(a => a.RotasSeguras)
                .WithOne(r => r.Abrigo)
                .HasForeignKey(r => r.AbrigoId)
                .OnDelete(DeleteBehavior.Cascade);

            // Alerta 1:N RotaSegura
            modelBuilder.Entity<Alerta>()
                .HasMany(a => a.RotasSeguras)
                .WithOne(r => r.Alerta)
                .HasForeignKey(r => r.AlertaId)
                .OnDelete(DeleteBehavior.Cascade);

            // Corrige o tipo do campo booleano para Oracle
            modelBuilder.Entity<Abrigo>()
                .Property(a => a.Ativo)
                .HasColumnType("NUMBER(1)");

            base.OnModelCreating(modelBuilder);
        }
    }
}
