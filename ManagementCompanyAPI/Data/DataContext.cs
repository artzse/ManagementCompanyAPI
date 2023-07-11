using Microsoft.EntityFrameworkCore;
using ManagementCompanyAPI.Models;

namespace ManagementCompanyAPI.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<Company> Companies { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Catalog> Catalogs { get; set; }
        public DbSet<Price> Prices { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Location>()
                .HasOne(e => e.Company)
                .WithMany()
                .HasForeignKey(e => e.Company);

            modelBuilder.Entity<Address>()
                .HasOne(e => e.Location)
                .WithMany()
                .HasForeignKey(e => e.Location);

            modelBuilder.Entity<Catalog>()
                .HasOne(e => e.Company)
                .WithMany()
                .HasForeignKey(e => e.Company);

            modelBuilder.Entity<Price>()
                .HasOne(e => e.Location)
                .WithMany()
                .HasForeignKey(e => e.Location);

            modelBuilder.Entity<Price>()
                .HasOne(e => e.Location)
                .WithMany()
                .HasForeignKey(e => e.Location);
        }
    }
}
