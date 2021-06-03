using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Simple_Book_Store.Data;
using Simple_Book_Store.Models;
using Simple_Book_Store.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Simple_Book_Store.Controllers
{
    public class ProductController : Controller
    {
        private readonly AppDbContext _appDbContext;
        private readonly IWebHostEnvironment _hostEnvironment;
        public ProductController(AppDbContext appDbContext, IWebHostEnvironment hostEnvironment)
        {
            _appDbContext = appDbContext;
            _hostEnvironment = hostEnvironment;
        }

        public async Task<IActionResult> Details(int id)
        {
            var product = await _appDbContext.Products.FirstOrDefaultAsync(p => p.ProductId == id);
            if (product == null)
                return NotFound();

            return View(new ProductViewModel
            {
                Product=product,
                ReturnUrl = Request.Headers["Referer"]
            });
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id)
        {
            var product = await _appDbContext.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.ProductId == id);

            if (product == null)
                return NotFound();

            var categories = await _appDbContext.Categories.ToListAsync();
            ViewData["CategoryId"] = new SelectList(categories, "CategoryId", "CategoryName", product.Category);

            return View(new EditProductViewModel
            {
                Product = product,
                ReturnUrl = Request.Headers["Referer"]
            });
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(EditProductViewModel model)
        {
            var categories = await _appDbContext.Categories.ToListAsync();
            ViewData["CategoryId"] = new SelectList(categories, "CategoryId", "CategoryName", model.Product.Category);

            if (ModelState.IsValid)
            {
                if (model.FormFile != null)
                {
                    var filePath = Path.Combine(Path.Combine(_hostEnvironment.WebRootPath, "images", "Books"), Path.GetFileName(model.FormFile.FileName));
                    var oldFileName = Path.GetFileName(model.Product.ImageUrl.Replace("/", "\\"));
                    try
                    {
                        if (oldFileName != model.FormFile.FileName)
                        {
                            var oldFilePath = Path.Combine(_hostEnvironment.WebRootPath, "images", "Books", oldFileName);

                            if (System.IO.File.Exists(oldFilePath))
                            {
                                System.IO.File.Delete(oldFilePath);
                            }
                        }

                        using (var fileStream = System.IO.File.Create(filePath))
                        {
                            await model.FormFile.CopyToAsync(fileStream);
                        }

                        model.Product.ImageUrl = "/" + Path.Combine("images", "Books", model.FormFile.FileName).Replace("\\", "/");

                        var thumbnailFilePath = Path.Combine(Path.Combine(_hostEnvironment.WebRootPath, "images", "Books_small"), Path.GetFileName(model.ThumbnailFormFile.FileName));
                        var oldThumbnailFileName = Path.GetFileName(model.Product.ImageThumbnailUrl.Replace("/", "\\"));
                        if (oldThumbnailFileName != model.FormFile.FileName)
                        {
                            var oldThumbnailFilePath = Path.Combine(_hostEnvironment.WebRootPath, "images", "Books", oldThumbnailFileName);

                            if (System.IO.File.Exists(oldThumbnailFilePath))
                            {
                                System.IO.File.Delete(oldThumbnailFilePath);
                            }
                        }

                        using (var fileStream = System.IO.File.Create(thumbnailFilePath))
                        {
                            await model.FormFile.CopyToAsync(fileStream);

                        }

                        model.Product.ImageUrl = "/" + Path.Combine("images", "Books", model.ThumbnailFormFile.FileName).Replace("\\", "/");
                    }
                    catch (Exception e)
                    {
                        ViewData["ChangesSavedMessage"] = $"{e.Message} Changes were not saved to the database. You can not choose an image from \\wwwroot\\images\\Books or \\wwwroot\\images\\Books_small.";
                        return View(model);
                    }
                }

                if (model.ThumbnailFormFile != null)
                {
                    var thumbnailFilePath = Path.Combine(Path.Combine(_hostEnvironment.WebRootPath, "images", "Books_small"), Path.GetFileName(model.ThumbnailFormFile.FileName));

                    var oldThumbnailFileName = Path.GetFileName(model.Product.ImageThumbnailUrl.Replace("/", "\\"));

                    try
                    {
                        if (oldThumbnailFileName != model.ThumbnailFormFile.FileName)
                        {
                            var oldThumbnailFilePath = Path.Combine(_hostEnvironment.WebRootPath, "images", "Books", oldThumbnailFileName);

                            if (System.IO.File.Exists(oldThumbnailFilePath))
                            {
                                System.IO.File.Delete(oldThumbnailFilePath);
                            }
                        }

                        using (var fileStream = System.IO.File.Create(thumbnailFilePath))
                        {
                            await model.ThumbnailFormFile.CopyToAsync(fileStream);

                        }

                        model.Product.ImageThumbnailUrl = "/" + Path.Combine("images", "Books", model.ThumbnailFormFile.FileName).Replace("\\", "/");
                    }
                    catch (Exception e)
                    {
                        ViewData["ChangesSavedMessage"] = $"{e.Message} Changes were not saved to the database. You can not choose an image from \\wwwroot\\images\\Books or \\wwwroot\\images\\Books_small.";
                        return View(model);
                    }
                }

                _appDbContext.Products.Update(model.Product);

                await _appDbContext.SaveChangesAsync();

                ViewData["ChangesSavedMessage"] = "Changes were saved to the database.";
            }
            else
            {
                ViewData["ChangesSavedMessage"] = "Changes were not saved to the database. The model is not valid.";
            }

            return View(model);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _appDbContext.Products
                .Include(p=>p.Category)
                .FirstOrDefaultAsync(p => p.ProductId == id);
            if (product == null)
                return NotFound();

            return View(new ProductViewModel
            {
                Product = product,
                ReturnUrl = Request.Headers["Referer"].ToString()
            });
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id, string returnUrl)
        {
            var product = await _appDbContext.Products
                .Include(p=>p.Category)
                .FirstOrDefaultAsync(p => p.ProductId == id);
            if (product == null)
                return NotFound();

            _appDbContext.Products.Remove(product);

            await _appDbContext.SaveChangesAsync();

            if (returnUrl.Contains("Details"))
            {
                return RedirectToRoute(new { controller = "Home", Action = "Index" });
            }

            return Redirect(returnUrl);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create()
        {
            var categories = await _appDbContext.Categories.ToListAsync();
            ViewData["CategoryId"] = new SelectList(categories, "CategoryId", "CategoryName");

            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(CreateProductViewModel model)
        {
            var categories = await _appDbContext.Categories.ToListAsync();
            ViewData["CategoryId"] = new SelectList(categories, "CategoryId", "CategoryName");

            if (ModelState.IsValid)
            {
                var filePath = Path.Combine(Path.Combine(_hostEnvironment.WebRootPath, "images", "Books"), Path.GetFileName(model.FormFile.FileName));
                var thumbnailFilePath = Path.Combine(Path.Combine(_hostEnvironment.WebRootPath, "images", "Books_small"), Path.GetFileName(model.ThumbnailFormFile.FileName));

                try
                {
                    using (var fileStream = System.IO.File.Create(filePath))
                    {
                        await model.FormFile.CopyToAsync(fileStream);
                        model.Product.ImageUrl = "/" + Path.Combine("images", "Books", model.FormFile.FileName).Replace("\\", "/");
                    }

                    using (var fileStream = System.IO.File.Create(thumbnailFilePath))
                    {
                        await model.ThumbnailFormFile.CopyToAsync(fileStream);
                        model.Product.ImageThumbnailUrl = "/" + Path.Combine("images", "Books_small", model.ThumbnailFormFile.FileName).Replace("\\", "/");
                    }
                }
                catch (Exception e)
                {
                    ViewData["CreateProductMessage"] = $"{e.Message} The product was not added to the database. You can not choose an image from \\wwwroot\\images\\Books or \\wwwroot\\images\\Books_small.";
                    return View(model);
                }

                _appDbContext.Products.Add(model.Product);

                await _appDbContext.SaveChangesAsync();

                TempData["CreateProductMessage"] = "The product was added to the database.";

                return RedirectToAction("Details", new { id = model.Product.ProductId });
            }

            ViewData["CreateProductMessage"] = "The product was not added to the database. The model is not valid";

            return View(model);
        }

        public async Task<IActionResult> Index(
            string categoryAuthorSaleProductOfTheWeek,
            string currentSelection,
            string currentFilter,
            string searchString,
            string sortByOrder,
            int? pageNumber)
        {
            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;
            ViewData["CategoryAuthorSaleProductOfTheWeek"] = categoryAuthorSaleProductOfTheWeek;
            ViewData["CurrentSelection"] = currentSelection;
            ViewData["SortByOrder"] = sortByOrder;

            var products = from p in _appDbContext.Products
                           select p;

            if (!String.IsNullOrEmpty(searchString))
            {
                // All books (*) or selection
                if (searchString != "*")
                {
                    products = products.Where(p => p.Author.Contains(searchString)
                        || p.Title.Contains(searchString));
                }
                if (products.Count() == 0)
                {
                    ViewData["Title"] = $"There are no products matching \"{searchString}\".";
                }
                else
                {
                    ViewData["Title"] = "Search string: " + searchString;
                    if (!String.IsNullOrEmpty(sortByOrder) && sortByOrder != "sortby")
                    {
                        ViewData["Title"] += " (order by: " + sortByOrder + ")";
                    }
                    switch (sortByOrder)
                    {
                        case "title_asc":
                            products = products.OrderBy(p => p.Title);
                            break;
                        case "title_desc":
                            products = products.OrderByDescending(p => p.Title);
                            break;
                        case "author_asc":
                            products = products.OrderBy(p => p.Author);
                            break;
                        case "author_desc":
                            products = products.OrderByDescending(p => p.Author);
                            break;
                        case "price_asc":
                            //products = products.OrderBy(p => p.Price);
                            products = products.OrderBy(p => (p.IsOnSale && p.PriceOnSale < p.Price) ? p.PriceOnSale : p.Price);
                            break;
                        case "price_desc":
                            //products = products.OrderByDescending(p => p.Price);
                            products = products.OrderByDescending(p => p.IsOnSale ? p.PriceOnSale : p.Price);
                            break;
                        default:
                            // "sortby" means default sort order = Product ID ascending
                            break;
                    }
                }
            }
            else if (currentSelection == "All products")
            {
                ViewData["Title"] = currentSelection;
            }
            else if (categoryAuthorSaleProductOfTheWeek == "Category")
            {
                products = products.Where(p => p.Category.CategoryName == currentSelection);
                if (products.Count() == 0)
                {
                    ViewData["Title"] = $"There are no products in category {currentSelection}.";
                }
                else
                {
                    ViewData["Title"] = currentSelection;
                }
            }
            else if (categoryAuthorSaleProductOfTheWeek == "Author")
            {
                products = products.Where(p => p.Author == currentSelection);
                if (products.Count() == 0)
                {
                    ViewData["Title"] = $"There are no products by author {currentSelection}.";
                }
                else
                {
                    ViewData["Title"] = currentSelection;
                }
            }
            else if (categoryAuthorSaleProductOfTheWeek == "Sale")
            {
                products = products.Where(p => p.IsOnSale);
                if (products.Count() == 0)
                {
                    ViewData["Title"] = "There are no products on sale.";
                }
                else
                {
                    ViewData["Title"] = "Sale";
                }
            }
            else if (categoryAuthorSaleProductOfTheWeek == "ProductOfTheWeek")
            {
                products = products.Where(p => p.IsProductOfTheWeek);
                if (products.Count() == 0)
                {
                    ViewData["Title"] = "There are no products of the week.";
                }
                else
                {
                    ViewData["Title"] = "Products of the week";
                }
            }
            else
            {
                return Redirect(Request.Headers["Referer"].ToString());
            }

            int pageSize = 3;
            if (products.Count() % pageSize == 0 && pageNumber > products.Count() / pageSize && products.Count() != 0)
            {
                pageNumber--;
                return RedirectToAction("Index", new { categoryAuthorSaleProductOfTheWeek, currentSelection, currentFilter, searchString, sortByOrder, pageNumber });
            }
            return View(await PaginatedList<Product>.CreateAsync(products.AsNoTracking(), pageNumber ?? 1, pageSize));
        }
    }
}