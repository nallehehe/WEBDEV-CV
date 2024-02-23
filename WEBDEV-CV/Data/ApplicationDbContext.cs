using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WEBDEV_CV.Models;

namespace WEBDEV_CV.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Skills> Skills { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsbuilder)
        {
            optionsbuilder.UseSqlServer("Insert connectionString");
        }
    }
}
