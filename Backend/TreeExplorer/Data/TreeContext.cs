using Microsoft.EntityFrameworkCore;
using TreeExplorer.Models;

namespace TreeExplorer.Data
{
    public class TreeContext : DbContext
    {
        public TreeContext(DbContextOptions<TreeContext> options)
           : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Element>()
                .HasOne(p => p.Usser)
                .WithMany(b => b.Elements)
                .IsRequired();

            modelBuilder.Entity<UsserData>()
                .HasOne(p => p.Usser)
                .WithMany(b => b.UsserDatas)
                .IsRequired();
        }

        public DbSet<TreeExplorer.Models.Usser> Ussers { get; set; }
        public DbSet<TreeExplorer.Models.Element> Elements { get; set; }
        public DbSet<TreeExplorer.Models.UsserData> UsserDatas { get; set; }
    }
}
