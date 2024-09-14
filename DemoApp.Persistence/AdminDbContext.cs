using Microsoft.EntityFrameworkCore;
using DemoApp.Domain.Models.Administration;

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
        public virtual DbSet<UserApplication> UserApplications { get; set; }
        public virtual DbSet<RoleAuthorization> RoleAuthorizations { get; set; }
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

            modelBuilder.Entity<RoleAuthorization>()
                .HasKey(ra => new { ra.RoleId, ra.AuthorizationId });

            modelBuilder.Entity<RoleAuthorization>()
                .HasOne(ra => ra.Role)
                .WithMany(r => r.RoleAuthorizations)
                .HasForeignKey(ra => ra.RoleId);

            modelBuilder.Entity<RoleAuthorization>()
                .HasOne(ra => ra.Authorization)
                .WithMany(a => a.RoleAuthorizations)
                .HasForeignKey(ra => ra.AuthorizationId);

            modelBuilder.Entity<UserApplication>(entity =>
            {
                entity.ToTable("UserApplication");
            });


            modelBuilder.Entity<UserApplication>()
                .HasKey(ua => new { ua.UserId, ua.ApplicationId, ua.RoleId });

            modelBuilder.Entity<User>()
                .HasMany(u => u.UserApplications)
                .WithOne(ua => ua.User)
                .HasForeignKey(ua => ua.UserId);

            modelBuilder.Entity<Application>()
                .HasMany(a => a.UserApplications)
                .WithOne(ua => ua.Application)
                .HasForeignKey(ua => ua.ApplicationId);

            modelBuilder.Entity<Role>()
                .HasMany(r => r.UserApplications)
                .WithOne(ua => ua.Role)
                .HasForeignKey(ua => ua.RoleId);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
