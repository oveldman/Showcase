using System;
using System.Text.Json.Serialization;

namespace Web.Models.Authentication
{
    public class BearerViewModel {
        [JsonPropertyName("Succes")]
        public bool Succes { get; set; }
        [JsonPropertyName("ErrorMessage")]
        public string ErrorMessage { get; set; }
        [JsonPropertyName("AccessToken")]
        public string AccessToken { get; set; }
        [JsonPropertyName("Type")]
        public string Type { get; set; }
        [JsonPropertyName("ExpiresIn")]        
        public string ExpiresIn { get; set; }
        [JsonPropertyName("Username")]        
        public string Username { get; set; }
        [JsonPropertyName("Issued")]
        public string Issued { get; set; }
        [JsonPropertyName("Expires")]
        public string Expires { get; set; }

    }
}