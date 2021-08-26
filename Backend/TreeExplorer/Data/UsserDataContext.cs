using Microsoft.EntityFrameworkCore;

namespace TreeExplorer.Data
{
    public class UsserDataContext : DbContext
    {
        public UsserDataContext(DbContextOptions<UsserDataContext> options)
           : base(options)
        {
        }

        public DbSet<TreeExplorer.Models.UsserData> UsserData { get; set; }
    }
}
