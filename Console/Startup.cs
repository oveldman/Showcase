using System;
using Microsoft.Extensions.Configuration;
using System.IO;
using Microsoft.EntityFrameworkCore;
using DataLayer;

namespace Console
{
    public sealed class Startup {
        private const bool Development = true;
        private static Startup _startup;
        private string _connection;
        public Inserter Inserter { get; private set; }
        private Startup() {}
        public static Startup Create() 
        {
            if (_startup == null) {
                _startup = new Startup();
            }

            return _startup;
        } 

        public void Load() 
        {
            string setting = "appsettings.json";

            if (Development) {
                setting = "appsettings.Development.json";
            }

            var builder = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(AppContext.BaseDirectory + "../../../../Server/Web"))
                .AddJsonFile(setting, true, true)
                .AddEnvironmentVariables();

            var config = builder.Build();
            _connection = config["ConnectionStrings:DefaultConnection"];

            CreateShowCaseContext();
        }

        private void CreateShowCaseContext() {
            var optionsBuilder = new DbContextOptionsBuilder<ShowCaseContext>();
                optionsBuilder.UseNpgsql(_connection);

            ShowCaseContext context = new ShowCaseContext(optionsBuilder.Options, new OperationalStoreOptionsMigrations());
            Inserter = new(context);
        }
    }
}