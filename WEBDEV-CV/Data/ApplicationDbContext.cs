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
            optionsbuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=aspnet-WEBDEV-CV-3cf68ec4-a0c5-426b-9d3c-b7b0cf6c3c0a;Trusted_Connection=True;MultipleActiveResultSets=true");
        }
    }
}
