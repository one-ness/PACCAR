﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PaccarAPI.Data;

namespace PaccarAPI.Migrations
{
    [DbContext(typeof(PaccarDbContext))]
    [Migration("20190304064127_collToList")]
    partial class collToList
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.1-servicing-10028")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("PaccarAPI.Models.BestPractice", b =>
                {
                    b.Property<int>("BestPracticeId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Date");

                    b.Property<string>("Department");

                    b.Property<string>("PN");

                    b.Property<string>("Practice");

                    b.Property<string>("Summary");

                    b.Property<string>("Title");

                    b.HasKey("BestPracticeId");

                    b.ToTable("BestPractice");
                });

            modelBuilder.Entity("PaccarAPI.Models.BestPracticeCompany", b =>
                {
                    b.Property<int>("BestPracticeId");

                    b.Property<int>("CompanyId");

                    b.HasKey("BestPracticeId", "CompanyId");

                    b.HasIndex("CompanyId");

                    b.ToTable("BestPracticeCompany");
                });

            modelBuilder.Entity("PaccarAPI.Models.Company", b =>
                {
                    b.Property<int>("CompanyId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.HasKey("CompanyId");

                    b.ToTable("Company");

                    b.HasData(
                        new
                        {
                            CompanyId = 1,
                            Name = "PACCAR"
                        },
                        new
                        {
                            CompanyId = 2,
                            Name = "Kenworth"
                        },
                        new
                        {
                            CompanyId = 3,
                            Name = "Peterbilt"
                        },
                        new
                        {
                            CompanyId = 4,
                            Name = "DAF"
                        });
                });

            modelBuilder.Entity("PaccarAPI.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.HasKey("UserId");

                    b.ToTable("User");
                });

            modelBuilder.Entity("PaccarAPI.Models.BestPracticeCompany", b =>
                {
                    b.HasOne("PaccarAPI.Models.BestPractice", "BestPractice")
                        .WithMany("BestPracticeCompanies")
                        .HasForeignKey("BestPracticeId")
                        .HasConstraintName("FK_BestPracticeId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("PaccarAPI.Models.Company", "Company")
                        .WithMany("CompanyBestPractices")
                        .HasForeignKey("CompanyId")
                        .HasConstraintName("FK_CompanyId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
