using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.EntityFrameworkCore;
using ShelterApi.Models;

namespace ShelterApi.Data
{
    public class ShelterDbContext : DbContext
    {
        public ShelterDbContext(DbContextOptions<ShelterDbContext> options) : base(options) { }
        public DbSet<Area> Areas => Set<Area>();
        public DbSet<Shelter> Shelters => Set<Shelter>();
        public DbSet<Inspection> Inspections => Set<Inspection>();
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Area>()
                    .HasMany(a => a.Shelters)
                    .WithOne(a => a.Area)
                    .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Shelter>()
                .HasMany(s => s.Inspections)
                .WithOne(s => s.Shelter)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Inspection>()
                .HasOne(i => i.Shelter)
                .WithMany(i => i.Inspections);
        }

    }
}
