using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Console
{
    public sealed class Startup {
        private const bool Development = true;
        private static Startup _startup;
        public string Connection { get; private set; }
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
            Connection = config["ConnectionStrings:DefaultConnection"];
        }
    }
}