using System.ComponentModel.DataAnnotations;

namespace Simple_Book_Store.Models
{
    public class Product
    {
        public int ProductId { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Author { get; set; }

        public string Description { get; set; }

        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        public Category Category { get; set; }
        
        [Required]
        [RegularExpression(@"\d+(\,\d{1,2})?", ErrorMessage = "Invalid price.")]
        public decimal Price { get; set; }

        [Display(Name = "Price on Sale")]
        [RegularExpression(@"\d+(\,\d{1,2})?", ErrorMessage = "Invalid price.")]
        public decimal PriceOnSale { get; set; }

        [Display(Name = "Product of the Week")]
        public bool IsProductOfTheWeek { get; set; }

        [Display(Name = "Sale")]
        public bool IsOnSale { get; set; }        
        
        public string ImageUrl { get; set; }

        public string ImageThumbnailUrl { get; set; }

        [Display(Name = "Show on front page")]
        public bool IsOnFrontPage { get; set; }
    }
}