using Simple_Book_Store.Data;
using Simple_Book_Store.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Simple_Book_Store.ViewModels;

namespace Simple_Book_Store.Controllers
{
    public class OrderController : Controller
    {
        private readonly ShoppingCart _shoppingCart;
        private readonly AppDbContext _appDbContext;
        private readonly UserManager<ApplicationUser> _userManager;
        private Order _order;
        public OrderController(ShoppingCart shoppingCart, AppDbContext applicationDbCOntext, UserManager<ApplicationUser> userManager, Order order)
        {
            _shoppingCart = shoppingCart;
            _appDbContext = applicationDbCOntext;
            _userManager = userManager;
            _order = order;
        }

        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> Checkout()
        {            
            if (_shoppingCart.ShoppingCartItems.Count == 0)
            {
                TempData["CartEmptyMessage"]="Your cart is empty, add some products first.";
                return RedirectToRoute(new { controller = "ShoppingCart", action = "index" });
            }

            var user = await _userManager.GetUserAsync(User);

            var order = new Order
            {
                FirstName=user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                AddressLine1 = user.AddressLine1,
                AddressLine2 = user.AddressLine2,
                ZipCode = user.ZipCode,
                City = user.City,
                Country = user.Country
            };

            return View(order);
        }

        [HttpPost]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> Checkout(Order order)
        {
            if (_shoppingCart.ShoppingCartItems.Count == 0)
            {
                ModelState.AddModelError("", "Your cart is empty, add some products first.");
            }

            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);

                if (user == null)
                {
                    return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
                }

                user.AddressLine1 = order.AddressLine1;
                user.AddressLine2 = order.AddressLine2;
                user.City = order.City;
                user.Country = order.Country;
                user.FirstName = order.FirstName;
                user.LastName = order.LastName;
                user.PhoneNumber = order.PhoneNumber;
                user.ZipCode = order.ZipCode;

                order.OrderDate = DateTime.Now;

                order.OrderTotal = _shoppingCart.ShoppingCartItems.
                Select(c => c.Product.IsOnSale ? c.Product.PriceOnSale * c.Quantity : c.Product.Price * c.Quantity).Sum();

                order.OrderItems = new List<OrderItem>();

                var shoppingCartItems = _shoppingCart.ShoppingCartItems;

                foreach (var shoppingCartItem in shoppingCartItems)
                {
                    var orderItem = new OrderItem
                    {
                        Quantity = shoppingCartItem.Quantity,
                        ProductId = shoppingCartItem.Product.ProductId,
                        Price = shoppingCartItem.Product.IsOnSale ? shoppingCartItem.Product.PriceOnSale : shoppingCartItem.Product.Price
                    };

                    order.OrderItems.Add(orderItem);
                }

                order.CustomerId = user.Id;

                await _appDbContext.Orders.AddAsync(order);

                await _appDbContext.SaveChangesAsync();

                HttpContext.Session.Remove("Cart");

                return RedirectToAction("CheckoutComplete", order);
            }

            return View(order);
        }

        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> CheckoutComplete(Order order)
        {
            if (ModelState.IsValid)
            {
                var localOrder = await _appDbContext.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product).FirstOrDefaultAsync(o => o.OrderId == order.OrderId);
                ViewBag.CheckoutCompleteMessage = "Thanks for your order!";

                return View(localOrder);
            }
            else
            {
                return View(order);
            }
            
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var order = await _appDbContext.Orders.FindAsync(id);

            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteOrderConfirmed(int id)
        {
            var order = await _appDbContext.Orders.FindAsync(id);

            if (order == null)
            {
                return NotFound();
            }

            _appDbContext.Orders.Remove(order);

            await _appDbContext.SaveChangesAsync();

            return RedirectToRoute(new { controller = "ApplicationUser", action = "ListOrders", id = order.CustomerId });
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id)
        {
            var order = await _appDbContext.Orders
                .FirstOrDefaultAsync(o => o.OrderId == id);

            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(Order order)
        {
            if (ModelState.IsValid)
            {
                _appDbContext.Orders.Update(order);

                await _appDbContext.SaveChangesAsync();

                TempData["ChangesSavedMessage"] = "Changes were saved to the database.";
            }
            else
            {
                TempData["ChangesSavedMessage"] = "Changes were not saved to the database.";
            }

            return RedirectToAction("Edit", new { id = order.OrderId });
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Details(int id)
        {
            var order = await _appDbContext.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .FirstOrDefaultAsync(o => o.OrderId == id);

            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ListOrderItems(int id)
        {
            var orderItems = await _appDbContext.OrderItems
                .Include(o=>o.Product)
                .Where(o => o.OrderId == id).ToListAsync();

            ViewData["OrderId"] = id;

            return View(orderItems);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteOrderItem(int id)
        {
            var orderItem = await _appDbContext.OrderItems
                .Include(oi => oi.Product).FirstOrDefaultAsync(oi => oi.OrderItemId == id);

            if (orderItem == null)
            {
                return NotFound();
            }

            return View(orderItem);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteOrderItemConfirmed(int id)
        {
            var orderItem = await _appDbContext.OrderItems.FindAsync(id);
            if (orderItem == null)
            {
                return NotFound();
            }

            _appDbContext.OrderItems.Remove(orderItem);

            var order = await _appDbContext.Orders.FindAsync(orderItem.OrderId);
            order.OrderTotal -= orderItem.Price * orderItem.Quantity;

            await _appDbContext.SaveChangesAsync();

            return RedirectToAction("ListOrderItems", new { id = orderItem.OrderId });
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DecreaseOrderItemQuantity(int id)
        {
            var orderItem = await _appDbContext.OrderItems.FindAsync(id);

            if (orderItem == null)
            {
                return NotFound();
            }

            if (orderItem.Quantity > 1)
            {
                orderItem.Quantity--;

                var order = await _appDbContext.Orders.FindAsync(orderItem.OrderId);

                if (order == null)
                {
                    return NotFound();
                }

                order.OrderTotal -= orderItem.Price;
            }
            else
            {
                _appDbContext.OrderItems.Remove(orderItem);
            }

            await _appDbContext.SaveChangesAsync();

            return RedirectToAction("ListOrderItems", new { id = orderItem.OrderId });
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> IncreaseOrderItemQuantity(int id)
        {
            var orderItem = await _appDbContext.OrderItems.FindAsync(id);

            if (orderItem == null)
            {
                return NotFound();
            }

            orderItem.Quantity++;

            var order = await _appDbContext.Orders.FindAsync(orderItem.OrderId);

            if (order == null)
            {
                return NotFound();
            }

            order.OrderTotal += orderItem.Price;

            await _appDbContext.SaveChangesAsync();

            return RedirectToAction("ListOrderItems", new { id = orderItem.OrderId });
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Browse(int id)
        {
            var order = await _appDbContext.Orders.FindAsync(id);

            _order.AddressLine1 = order.AddressLine1;
            _order.AddressLine2 = order.AddressLine2;
            _order.City = order.City;
            _order.Country = order.Country;
            _order.CustomerId = order.CustomerId;
            _order.Email = order.Email;
            _order.FirstName = order.FirstName;
            _order.IsShipped = order.IsShipped;
            _order.LastName = order.LastName;
            _order.OrderDate = order.OrderDate;
            _order.OrderId = order.OrderId;
            _order.OrderTotal = order.OrderTotal;
            _order.PhoneNumber = order.PhoneNumber;
            _order.ZipCode = order.ZipCode;

            return RedirectToRoute(new { controller = "Home", action = "Index" });
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddToOrder(int id)
        {
            if (_order.OrderId > 0)
            {
                var product = await _appDbContext.Products.FindAsync(id);

                //var price = product.IsOnSale ? product.Price : product.PriceOnSale;
                var price = product.IsOnSale ? product.PriceOnSale : product.Price;

                var orderItem =
                await _appDbContext.OrderItems.SingleOrDefaultAsync(
                    oi => oi.Product.ProductId == product.ProductId && oi.OrderId == _order.OrderId);

                if (orderItem == null)
                {
                    orderItem = new OrderItem
                    {
                        OrderId = _order.OrderId,
                        ProductId = id,
                        Price = price,
                        Quantity = 1
                    };

                    await _appDbContext.OrderItems.AddAsync(orderItem);
                }
                else
                {
                    orderItem.Quantity++;
                }

                _order.OrderTotal += price;

                _order.OrderItems = await _appDbContext.OrderItems.Where(o => o.OrderId == _order.OrderId).ToListAsync();

                _appDbContext.Orders.Update(_order);

                await _appDbContext.SaveChangesAsync();

                return RedirectToAction("ListOrderItems", new { id = _order.OrderId });
            }

            return RedirectToRoute(new { controller = "ApplicationUser", action = "Index" });
        }

        public async Task<IActionResult> Save(int id)
        {
            var order = await _appDbContext.Orders
                .Include(o=>o.OrderItems)
                .FirstOrDefaultAsync(o=>o.OrderId==id);

            if (order == null)
            {
                return NotFound();
            }

            if (order.OrderItems.Count() == 0)
            {
                _appDbContext.Remove(order);
                await _appDbContext.SaveChangesAsync();
                return RedirectToRoute(new { controller = "ApplicationUser", action = "ListOrders", id = order.CustomerId });
            }

            return RedirectToAction("Edit", new { id=order.OrderId });
        }
    }
}
