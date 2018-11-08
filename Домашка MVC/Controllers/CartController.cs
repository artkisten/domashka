using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shop.Data;
using Shop.ViewModel;

namespace Shop.Controllers
{
    public class CartController : Controller
    {
        private readonly ShopContext _context;

        public CartController(ShopContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var cart = _context.Orders.Include(x => x.Items)
                .ThenInclude(x => x.Product).Where(x => x.Status == "New").FirstOrDefault();

            if (cart == null) { return View(); }

            var viewcart = new OrderViewModel
            {
                Number = cart.Number,
                Items = cart.Items
                        .Select(item => new OrderItemViewModel
                        {
                            ProductId = item.Product.Id,
                            ProductName = item.Product.Name,
                            ProductPrice = item.Product.Price,
                            Count = item.Count
                        }).ToList()
            };
            
            return View(viewcart);
        }
        public IActionResult Delete(int deleteid)
        {
            var product = _context.OrderItems.Where(x => x.ProductId == deleteid).FirstOrDefault();
            if (product != null)
            {
                product.Count--;
                if (product.Count<=0) { _context.Orders.Where(x => x.Status == "New").FirstOrDefault().Items.Remove(product); }
                _context.SaveChanges();
            }
            
            return RedirectToAction("Index","Cart");
        }
        public IActionResult CompleteOrder()
        {
            _context.Orders.Where(x => x.Status == "New").FirstOrDefault().Status = "Complete";
            _context.SaveChanges();
            return RedirectToAction("List","Order");
        }
    }
}