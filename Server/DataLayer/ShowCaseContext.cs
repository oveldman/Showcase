using System;
using System.Threading.Tasks;
using DataLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace DataLayer
{
    public class ShowCaseContext : DbContext
    {
        public ShowCaseContext(DbContextOptions<ShowCaseContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasPostgresExtension("uuid-ossp")
                   .Entity<ResumeInfo>()
                   .Property(ri => ri.ID)
                   .HasDefaultValueSql("uuid_generate_v4()");
        }

        public DbSet<ResumeInfo> ResumeInfos { get; set; }
        
    }
}