// using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
// using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PaccarAPI.Models;

namespace PaccarAPI.Data
{
    public class PaccarDbContext : DbContext
    {
        public PaccarDbContext(DbContextOptions<PaccarDbContext> options)
            : base(options) {}

        public DbSet<User> User { get; set; }
        public DbSet<BestPractice> BestPractice { get; set; }
        public DbSet<BestPracticeCompany> BestPracticeCompany { get; set; }
        public DbSet<Company> Company { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // set composite key for linking BP_Company table using Fluent API
            modelBuilder.Entity<BestPracticeCompany>()
            .HasKey(c => new { c.BestPracticeId, c.CompanyId });

            // seed the Company table
            modelBuilder.Entity<Company>().HasData(
                new Company() {Id = 1, Name = "PACCAR"},
                new Company() {Id = 2, Name = "Kenworth"},
                new Company() {Id = 3, Name = "Peterbilt"},
                new Company() {Id = 4, Name = "DAF"}
            );
        }
    }
}