using Microsoft.EntityFrameworkCore;

namespace TreeExplorer.Data
{
    public class TreeExplorerContext : DbContext
    {
        public TreeExplorerContext (DbContextOptions<TreeExplorerContext> options)
            : base(options)
        {
        }

        public DbSet<TreeExplorer.Models.Element> Element { get; set; }
    }
}
