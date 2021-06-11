using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TreeExplorer.Models;

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
