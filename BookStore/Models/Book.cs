using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirstProject.Models
{
    public class Book
    {
        public int id { get; set; }
        public string Tittle { get; set; }
        public string Description { get; set; }
        public string ImageUpload { get; set; }
        public Author Author { get; set; }
    }
}
