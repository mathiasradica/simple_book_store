using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Simple_Book_Store.Models;
using Microsoft.AspNetCore.Mvc;
using Simple_Book_Store.Data;

namespace Simple_Book_Store.Components
{
    public class CategoryMenu : ViewComponent
    {
        private readonly AppDbContext _appDbContext;
        public CategoryMenu(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IViewComponentResult Invoke()
        {
            var categories = _appDbContext.Categories.OrderBy(c => c.CategoryName);
            return View(categories);
        }
    }
}
