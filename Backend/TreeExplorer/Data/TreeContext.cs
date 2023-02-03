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
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<UsserData>()
                .HasOne(p => p.Usser)
                .WithMany(b => b.UsserDatas)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade); 
        }

        public DbSet<Usser> Ussers { get; set; }
        public DbSet<Element> Elements { get; set; }
        public DbSet<UsserData> UsserDatas { get; set; }
    }
}
