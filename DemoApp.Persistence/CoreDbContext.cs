using Microsoft.EntityFrameworkCore;

namespace DemoApp.Persistence
{
    public partial class CoreDbContext : DbContext
    {
        public CoreDbContext() { }

        public CoreDbContext(DbContextOptions<CoreDbContext> options) 
            : base(options) { }
    }
}
