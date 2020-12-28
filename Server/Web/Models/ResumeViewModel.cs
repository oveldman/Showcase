using System;
using System.Text.Json.Serialization;

namespace Web.Models
{
    public class ResumeViewModel {
        [JsonPropertyName("ProfileName")]
        public string ProfileName { get; set; }
    }
}