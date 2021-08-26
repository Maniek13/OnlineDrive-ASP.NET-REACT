using Microsoft.EntityFrameworkCore;

namespace TreeExplorer.Data
{
    public class ElementContext : DbContext
    {
        public ElementContext (DbContextOptions<ElementContext> options)
            : base(options)
        {
        }

        public DbSet<TreeExplorer.Models.Element> Element { get; set; }
    }
}
