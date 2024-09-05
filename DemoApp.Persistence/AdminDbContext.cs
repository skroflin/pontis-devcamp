using Microsoft.EntityFrameworkCore;
using DemoApp.Core.Models.Administration;

namespace DemoApp.Persistence
{
    public partial class AdminDbContext : DbContext
    {
        public AdminDbContext() { }
        public AdminDbContext(DbContextOptions<AdminDbContext> options) 
            : base(options) { }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Application> Applications { get; set; }
        public virtual DbSet<Authorization> Authorizations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable(tb => tb.HasTrigger("trg_Users_History"));
            });

            OnModelCreatingPartial(modelBuilder);

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable(tb => tb.HasTrigger("trg_Roles_History"));
            });

            OnModelCreatingPartial(modelBuilder);

            modelBuilder.Entity<Application>(entity =>
            {
                entity.ToTable(tb => tb.HasTrigger("trg_Applications_History"));
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
