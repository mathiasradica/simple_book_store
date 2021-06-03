using Simple_Book_Store.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Simple_Book_Store.Models
{
    public class ShoppingCart
    {
        public List<ShoppingCartItem> ShoppingCartItems { get; set; }
        
        public static ShoppingCart GetCart(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?
                .HttpContext.Session;

            var shoppingCart = session.Get<ShoppingCart>("Cart") ?? new ShoppingCart() { ShoppingCartItems=new List<ShoppingCartItem>() };

            return shoppingCart;
        }
    }
}
