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

        public DbSet<User> Users { get; set; }
        public DbSet<BestPractice> BestPractices { get; set; }
    }
}