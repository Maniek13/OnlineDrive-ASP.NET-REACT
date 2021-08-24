using Microsoft.EntityFrameworkCore;

namespace TreeExplorer.Data
{
    public class UsserContext : DbContext
    {
        public UsserContext(DbContextOptions<UsserContext> options)
           : base(options)
        {
        }

        public DbSet<TreeExplorer.Models.Usser> Usser { get; set; }
    }
}
