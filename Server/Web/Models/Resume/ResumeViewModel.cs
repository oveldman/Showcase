using System;
using System.Text.Json.Serialization;

namespace Web.Models.Resume
{
    public class ResumeViewModel {
        [JsonPropertyName("LivingPlace")]
        public string LivingPlace { get; set; }
        [JsonPropertyName("Nationality")]
        public string Nationality { get; set; }
        [JsonPropertyName("ProfileName")]
        public string ProfileName { get; set; }
    }
}