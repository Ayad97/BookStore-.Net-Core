using FirstProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace BookStore.ViewModel
{
    public class BookRepositoryViewModel
    {
        public int id { get; set; }
        [Required]
        [MaxLength(20)]
        [MinLength(3)]
        public string Tittle { get; set; }
        [Required]
        public string Description { get; set; }
        public int AuthorId { get; set; }
         public IFormFile file { get; set; }
        public string imgView { get; set; }
        public List<Author> author { get; set; }
    }
}
