using System;
using System.Text.Json.Serialization;

namespace Web.Models.Authentication
{
    public class LoginViewModel {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}