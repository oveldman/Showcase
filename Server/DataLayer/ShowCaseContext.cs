using System;
using System.Threading.Tasks;
using DataLayer.Models;
using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace DataLayer
{
    public class ShowCaseContext : ApiAuthorizationDbContext<ShowCaseUser>
    {

        public ShowCaseContext(DbContextOptions options, IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.HasPostgresExtension("uuid-ossp")
                   .Entity<ResumeInfo>()
                   .Property(ri => ri.ID)
                   .HasDefaultValueSql("uuid_generate_v4()");
        }

        public DbSet<ResumeInfo> ResumeInfos { get; set; }
        
    }
}