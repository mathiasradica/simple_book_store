using Simple_Book_Store.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Simple_Book_Store.Data;
using Microsoft.EntityFrameworkCore;
using Simple_Book_Store.Models;

namespace Simple_Book_Store.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _appDbContext;

        public HomeController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<IActionResult> Index(int? pageNumber)
        {
            var products =  _appDbContext.Products.Where(p => p.IsOnFrontPage);

            int pageSize = 3;
            return View(await PaginatedList<Product>.CreateAsync(products.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        //Test
        public IActionResult Privacy()
        {
            return View();
        }
    }
}
