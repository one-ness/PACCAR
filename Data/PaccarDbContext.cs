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
            // BestPracticeCompany Table Properties
            modelBuilder.Entity<BestPracticeCompany>()
            .HasOne(bpc => bpc.BestPractice)
            .WithMany(bp => bp.BestPracticeCompanies)
            .HasForeignKey(bpc => bpc.BestPracticeId)
            .HasConstraintName("FK_BestPracticeId");

            modelBuilder.Entity<BestPracticeCompany>()
            .HasOne(bpc => bpc.Company)
            .WithMany(c => c.CompanyBestPractices)
            .HasForeignKey(bpc => bpc.CompanyId)
            .HasConstraintName("FK_CompanyId");

            modelBuilder.Entity<BestPracticeCompany>()
            .HasKey(c => new { c.BestPracticeId, c.CompanyId });

            // seed the Company table
            modelBuilder.Entity<Company>().HasData(
                new Company() {CompanyId = 1, Name = "PACCAR"},
                new Company() {CompanyId = 2, Name = "Kenworth"},
                new Company() {CompanyId = 3, Name = "Peterbilt"},
                new Company() {CompanyId = 4, Name = "DAF"}
            );
        }
    }
}