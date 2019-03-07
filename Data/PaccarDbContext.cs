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
                optionsBuilder.UseMySQL("Server=127.0.0.1;port=3306;Database=PaccarDB;User Id=root;Password=StarFox-91;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.1-servicing-10028");

            modelBuilder.Entity<BestPractice>(entity =>
            {
                entity.ToTable("BestPractice", "PaccarDB");

                entity.Property(e => e.BestPracticeId)
                    .HasColumnName("BestPracticeID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Company)
                    .IsRequired()
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.Date)
                    .IsRequired()
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.Department)
                    .IsRequired()
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.Pn)
                    .HasColumnName("PN")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.Practice)
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.Summary)
                    .IsRequired()
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(45)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<BestPracticeCompany>(entity =>
            {
                entity.HasKey(e => new { e.BestPracticeId, e.CompanyId });

                entity.ToTable("BestPractice_Company", "PaccarDB");

                entity.HasIndex(e => e.CompanyId)
                    .HasName("CompanyID_idx");

                entity.Property(e => e.BestPracticeId)
                    .HasColumnName("BestPracticeID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CompanyId)
                    .HasColumnName("CompanyID")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.BestPractice)
                    .WithMany(p => p.BestPracticeCompany)
                    .HasForeignKey(d => d.BestPracticeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("BestPracticeID");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.BestPracticeCompany)
                    .HasForeignKey(d => d.CompanyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("CompanyID");
            });

            modelBuilder.Entity<Company>(entity =>
            {
                entity.ToTable("Company", "PaccarDB");

                entity.Property(e => e.CompanyId)
                    .HasColumnName("CompanyID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Name)
                    .HasMaxLength(45)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Company>().HasData(
                new {CompanyId = 1, Name = "PACCAR"},
                new {CompanyId = 2, Name = "Kenworth"},
                new {CompanyId = 3, Name = "Peterbilt"},
                new {CompanyId = 4, Name = "DAF"}
            );
        }
    }
}
