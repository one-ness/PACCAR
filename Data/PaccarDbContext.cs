using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using PaccarAPI.Models;

namespace PaccarAPI.Data
{
    public partial class PaccarDbContext : DbContext
    {
        public PaccarDbContext()
        {
        }

        public PaccarDbContext(DbContextOptions<PaccarDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<BestPractice> BestPractice { get; set; }
        public virtual DbSet<BestPracticeCompany> BestPracticeCompany { get; set; }
        public virtual DbSet<Company> Company { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=localhost,1433;Database=PaccarDB;User Id=sa;Password=StarShooter91;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.1-servicing-10028");

            modelBuilder.Entity<BestPractice>(entity =>
            {
                entity.Property(e => e.Pn).HasColumnName("PN");
            });

            modelBuilder.Entity<BestPracticeCompany>(entity =>
            {
                entity.HasKey(e => new { e.BestPracticeId, e.CompanyId });

                entity.HasIndex(e => e.CompanyId);

                entity.HasOne(d => d.BestPractice)
                    .WithMany(p => p.BestPracticeCompany)
                    .HasForeignKey(d => d.BestPracticeId)
                    .HasConstraintName("FK_BestPracticeId");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.BestPracticeCompany)
                    .HasForeignKey(d => d.CompanyId)
                    .HasConstraintName("FK_CompanyId");
            });
        }
    }
}
