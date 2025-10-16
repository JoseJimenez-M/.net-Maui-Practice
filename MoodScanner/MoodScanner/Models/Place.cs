using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoodScanner.Models
{
    public class Place
    {
        public string Name { get; set; }
        public string Category { get; set; }
        public string Address { get; set; }
        public string ImageUrl { get; set; }
        public double Distance { get; set; }
    }
}

