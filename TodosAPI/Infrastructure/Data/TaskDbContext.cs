using Microsoft.EntityFrameworkCore;
using TodosAPI.Core.Entities;

namespace TodosAPI.Infrastructure.Data
{
    public class TaskDbContext : DbContext
    {
        public TaskDbContext(DbContextOptions<TaskDbContext> options) : base(options)
        {
        }

   

        public DbSet<Tarefa> Tasks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Tarefa>().ToTable("tarefa");
            modelBuilder.Entity<Tarefa>().HasKey(t => t.TarefaId);
            modelBuilder.Entity<Tarefa>().Property(t => t.TarefaId).HasColumnName("TarefaId");
            modelBuilder.Entity<Tarefa>().Property(t => t.Title).HasColumnName("Title");
            modelBuilder.Entity<Tarefa>().Property(t => t.Description).HasColumnName("Description");
            //modelBuilder.Entity<Tarefa>().Property(t => t.UserId).HasColumnName("UserId");
            modelBuilder.Entity<Tarefa>().Property(t => t.CreatedAt).HasColumnName("CreatedAt");
            modelBuilder.Entity<Tarefa>().Property(t => t.CompletedAt).HasColumnName("CompletedAt");
            modelBuilder.Entity<Tarefa>()
            .HasOne(t => t.User) // Navigation property
            .WithMany(u => u.Tasks)
            .HasConstraintName("FK_Tarefa_Users_UserId");
        }
    }
}
