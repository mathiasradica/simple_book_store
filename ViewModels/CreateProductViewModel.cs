using Microsoft.AspNetCore.Http;
using Simple_Book_Store.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Simple_Book_Store.ViewModels
{
    public class CreateProductViewModel
    {
        public Product Product { get; set; }

        [Required]
        [Display(Name = "Image")]
        public IFormFile FormFile { get; set; }

        [Required]
        [Display(Name = "Thumbnail Image")]
        public IFormFile ThumbnailFormFile { get; set; }
    }
}
