using System;
using System.Threading.Tasks;
using DataLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace DataLayer
{
    public class ShowCaseContext : DbContext
    {
        private readonly string _connectionString;
        private readonly bool _startUpAvailable;

        public ShowCaseContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        public ShowCaseContext(DbContextOptions<ShowCaseContext> options) : base(options)
        {
            _startUpAvailable = true;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (_startUpAvailable) 
            {
                base.OnConfiguring(optionsBuilder);
            } 
            else 
            {
                optionsBuilder.UseNpgsql(_connectionString);
            }
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