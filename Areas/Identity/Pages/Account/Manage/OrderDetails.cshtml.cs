using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Simple_Book_Store.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Simple_Book_Store.Models;
using Microsoft.EntityFrameworkCore;

namespace Simple_Book_Store.Areas.Identity.Pages.Account.Manage
{
    public partial class OrderDetailsModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly AppDbContext _appDbContext;
        public OrderDetailsModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            AppDbContext appDbContext)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _appDbContext = appDbContext;
        }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            public Order Order { get; set; }
        }

        private void Load(Order order)
        {
            Input = new InputModel
            {
                Order = order
            };
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var order = await _appDbContext.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi=>oi.Product)
                .FirstOrDefaultAsync(o => o.OrderId == id);

            if (order == null)
            {
                return NotFound($"Unable to load order with ID '{id}'.");
            }

            var user = await _userManager.GetUserAsync(User);
            if (order.CustomerId != user.Id)
            {
                return RedirectToPage("OrderHistory");
            }

            Load(order);
            return Page();
        }
    }
}
