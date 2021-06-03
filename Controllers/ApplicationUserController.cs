using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Simple_Book_Store.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Simple_Book_Store.ViewModels;
using Microsoft.AspNetCore.Http;

namespace Simple_Book_Store.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ApplicationUserController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly AppDbContext _appDbContext;

        public ApplicationUserController(UserManager<ApplicationUser> userManager, AppDbContext appDbContext)
        {
            _userManager = userManager;
            _appDbContext = appDbContext;
        }


        public async Task<IActionResult> Index(
            string sortOrder,
            string currentFilter,
            string searchString,
            int? pageNumber)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";

            var users = from u in _appDbContext.Users
                        select u;

            if (sortOrder == "name_desc")
            {
                users = users.OrderByDescending(u => u.LastName);
            }
            else
            {
                users = users.OrderBy(u => u.LastName);
            }

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;

            users = users.Where(u => u.LastName.Contains(searchString)
                               || u.FirstName.Contains(searchString)
                               || u.Email.Contains(searchString));

            int pageSize = 3;
            return View(await PaginatedList<ApplicationUser>.CreateAsync(users.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        public async Task<IActionResult> Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            HttpContext.Session.SetString("ReturnUrl", Request.Headers["Referer"]);

            return View(user);
        }

        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            await _userManager.DeleteAsync(user);

            return Redirect(HttpContext.Session.GetString("ReturnUrl"));
        }

        public async Task<IActionResult> Edit(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            HttpContext.Session.SetString("ReturnUrl", Request.Headers["Referer"]);

            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ApplicationUser applicationUser)
        {
            if (string.IsNullOrEmpty(applicationUser.Id))
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(applicationUser.Id);

                user.FirstName = applicationUser.FirstName;
                user.LastName = applicationUser.LastName;
                user.AddressLine1 = applicationUser.AddressLine1;
                user.AddressLine2 = applicationUser.AddressLine2;
                user.City = applicationUser.City;
                user.Country = applicationUser.Country;
                user.ZipCode = applicationUser.ZipCode;
                user.PhoneNumber = applicationUser.PhoneNumber;
                user.Email = applicationUser.Email;

                await _userManager.UpdateAsync(user);

                TempData["ChangesSavedMessage"] = "Changes were saved to the database.";
            }
            else
            {
                TempData["ChangesSavedMessage"] = "Changes were not saved to the database.";
            }

            return RedirectToAction("Edit", new { id = applicationUser.Id });
        }

        public async Task<IActionResult> ListOrders(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }
            var email = await (from user in _userManager.Users
                               where user.Id == id
                               select user.Email).SingleOrDefaultAsync();

            var orders = await _appDbContext.Orders.Where(o => o.CustomerId == id).ToListAsync();

            if (Request.Headers["Referer"].ToString().Contains("searchString"))
            {
                HttpContext.Session.SetString("ReturnUrl", Request.Headers["Referer"]);
            }

            return View(orders);
        }

        public async Task<IActionResult> Details(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            HttpContext.Session.SetString("ReturnUrl", Request.Headers["Referer"]);

            return View(user);
        }
    }
}
