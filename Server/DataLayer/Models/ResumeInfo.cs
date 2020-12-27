using System;
using System.ComponentModel.DataAnnotations;

namespace DataLayer.Models
{
    public class ResumeInfo
    {
        [Key]
        public Guid ID { get; set; }
        public string Name { get; set;}
    }
}
