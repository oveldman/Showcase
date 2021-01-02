using System;
using System.Collections.Generic;

namespace Web.Models
{
    public class GeneralResponse
    {
        public bool Succes { get; set; }
        public List<string> ErrorMessages { get; set; } = new();
    }
}
