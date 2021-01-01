using System;
using System.Text.Json.Serialization;

namespace Web.Models.Authentication
{
    public class BearerViewModel {
        public bool Succes { get; set; }
        public string ErrorMessage { get; set; }
        public string AccessToken { get; set; }
        public string Type { get; set; }
        public string ExpiresIn { get; set; }
        public string Username { get; set; }
        public string Issued { get; set; }
        public string Expires { get; set; }

    }
}