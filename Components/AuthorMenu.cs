using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Simple_Book_Store.Models;
using Microsoft.AspNetCore.Mvc;
using Simple_Book_Store.Data;

namespace Simple_Book_Store.Components
{
    public class AuthorMenu : ViewComponent
    {
        private readonly AppDbContext _appDbContext;
        public AuthorMenu(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IViewComponentResult Invoke()
        {
            var authors = (from product in _appDbContext.Products
                           select product.Author).OrderBy(a=>a).Distinct();

            return View(authors);
        }
    }
}
