using DemoApp.Domain.CoreDbEntities;
using DemoApp.Domain.Models.Common;
using DemoApp.Domain.Models.Geolocation;
using Microsoft.EntityFrameworkCore;

namespace DemoApp.Persistence
{
    public partial class CoreDbContext : DbContext
    {
        public CoreDbContext() { }

        public CoreDbContext(DbContextOptions<CoreDbContext> options) 
            : base(options) { }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Gender> Genders { get; set; }
        public virtual DbSet<NationalIdType> NationalIdType { get; set; }
        public virtual DbSet<Country> Country { get; set; }
        public virtual DbSet<District> Districts { get; set; }
        public virtual DbSet<Place> Places { get; set; }
        public virtual DbSet<Region> Region { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>(entity =>
            {
                entity.ToTable(tb => tb.HasTrigger("trg_Employees_History"));
            });

            OnModelCreatingPartial(modelBuilder);

            modelBuilder.Entity<Place>(entity =>
            {
                entity.ToTable(tb => tb.HasTrigger("trg_Places_History"));
            });

            OnModelCreatingPartial(modelBuilder);

            modelBuilder.Entity<Region>(entity =>
            {
                entity.ToTable(tb => tb.HasTrigger("trg_Regions_History"));
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
