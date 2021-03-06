using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Simple_Book_Store.Models;

namespace Simple_Book_Store.ViewModels
{
    public class ProductsListViewModel
    {
        public IEnumerable<Product> Products { get; set; }
        public string CurrentSelection { get; set; }
    }
}
