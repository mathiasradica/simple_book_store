using Simple_Book_Store.Data;
using Simple_Book_Store.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Simple_Book_Store.ViewModels;

namespace Simple_Book_Store.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly ShoppingCart _shoppingCart;
        private readonly AppDbContext _appDbContext;
        public ShoppingCartController(ShoppingCart shoppingCart, AppDbContext appDbContext)
        {
            _shoppingCart = shoppingCart;
            _appDbContext = appDbContext;
        }

        public IActionResult Index()
        {
            var shoppingCartViewModel = new ShoppingCartViewModel
            {
                ShoppingCart = _shoppingCart,
                ShoppingCartTotal = _shoppingCart.ShoppingCartItems
                .Select(c => c.Product.IsOnSale ? c.Product.PriceOnSale * c.Quantity : c.Product.Price * c.Quantity).Sum()
            };

            return View(shoppingCartViewModel);
        }

        public IActionResult AddToShoppingCart(int id)
        {
            var selectedProduct = _appDbContext.Products.FirstOrDefault(p => p.ProductId == id);

            if (selectedProduct != null)
            {
                var shoppingCartItem =
                _shoppingCart.ShoppingCartItems.FirstOrDefault(
                    s => s.Product.ProductId == selectedProduct.ProductId);

                if (shoppingCartItem == null)
                {
                    shoppingCartItem = new ShoppingCartItem
                    {
                        Product = selectedProduct,
                        Quantity = 1
                    };

                    _shoppingCart.ShoppingCartItems.Add(shoppingCartItem);
                }
                else
                {
                    shoppingCartItem.Quantity++;
                }

                HttpContext.Session.Set<ShoppingCart>("Cart", _shoppingCart);
            }

            return Redirect(Request.Headers["Referer"].ToString());
        }

        public RedirectToActionResult RemoveFromShoppingCart(int id)
        {
            var selectedProduct = _appDbContext.Products.SingleOrDefault(p => p.ProductId == id);

            if (selectedProduct != null)
            {
                var shoppingCartItem =
                _shoppingCart.ShoppingCartItems.FirstOrDefault(
                    s => s.Product.ProductId == selectedProduct.ProductId);

                _shoppingCart.ShoppingCartItems.Remove(shoppingCartItem);

                HttpContext.Session.Set<ShoppingCart>("Cart", _shoppingCart);
            }

            return RedirectToAction("Index");
        }

        public IActionResult IncreaseShoppingCartItemQuantity(int id)
        {
            var shoppingCartItem = _shoppingCart.ShoppingCartItems.FirstOrDefault(s => s.Product.ProductId == id);

            if (shoppingCartItem == null)
            {
                return NotFound();
            }

            shoppingCartItem.Quantity++;

            HttpContext.Session.Set<ShoppingCart>("Cart", _shoppingCart);

            return RedirectToAction("Index");
        }

        public IActionResult DecreaseShoppingCartItemQuantity(int id)
        {
            var shoppingCartItem = _shoppingCart.ShoppingCartItems.FirstOrDefault(s => s.Product.ProductId == id);

            if (shoppingCartItem == null)
            {
                return NotFound();
            }

            if (shoppingCartItem.Quantity > 1)
            {
                shoppingCartItem.Quantity--;
            }
            else
            {
                _shoppingCart.ShoppingCartItems.Remove(shoppingCartItem);
            }

            HttpContext.Session.Set<ShoppingCart>("Cart", _shoppingCart);

            return RedirectToAction("Index");
        }
    }
}

