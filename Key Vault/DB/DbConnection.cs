using Key_Vault.Models;
using Microsoft.EntityFrameworkCore;

namespace Key_Vault.DB
{
    public class DbConnection : DbContext
    {
        public DbConnection(DbContextOptions options) : base(options)
        {
        }
        public DbSet<User> users { get; set; }
        public DbSet<Logs> logs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>().Property(a => a.email)
                .IsRequired();
            modelBuilder.Entity<User>().Property(a => a.password)
                .IsRequired();
            modelBuilder.Entity<User>().HasIndex(a => a.email);
            modelBuilder.Entity<User>()
               .HasOne(a => a.logs)
               .WithOne(a => a.user)
               .HasForeignKey<Logs>(a =>a.UserId)
               .IsRequired();
                
                
        }
    }
}
