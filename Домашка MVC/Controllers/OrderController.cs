using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Shop.Data;
using Shop.Models;
using Shop.ViewModel;

namespace Shop.Controllers
{
    public class OrderController : Controller
    {

        private readonly ShopContext _context;

        public OrderController(ShopContext context)
        {
            _context = context;
        }

        // GET: Order
        public async Task<IActionResult> List()
        {

            var orders = await _context.Orders
                .Include(x => x.Items)
                .ThenInclude(x => x.Product)
                .Where(x => x.Status!="New")
                .ToListAsync();

            var ordersViewModel = orders
                .Select(order => new OrderViewModel
                {
                    Number = order.Number,
                    Items = order.Items
                        .Select(item => new OrderItemViewModel
                        {
                            ProductName = item.Product.Name,
                            ProductPrice = item.Product.Price,
                            Count = item.Count
                        }).ToList()
                });

            return View(ordersViewModel);

        }

        public IActionResult Index()
        {
            var viewproducts = new List<ProductViewModel>();
            var cart = _context.Orders.Include(x => x.Items).ThenInclude(x => x.Product).Where(x => x.Status == "New").Select(x => x.Items).FirstOrDefault();
            var products = _context.Products.ToList();
            if (cart != null) {viewproducts  = products.Select(prod => new ProductViewModel { Id = prod.Id, Name = prod.Name, Price = prod.Price, InCart = cart.Where(x => x.ProductId == prod.Id).Select(x => x.Count).FirstOrDefault() }).ToList(); }
            else {viewproducts = products.Select(prod => new ProductViewModel { Id = prod.Id, Name = prod.Name, Price = prod.Price, InCart = 0 }).ToList(); }
            return View(viewproducts);
 
        }

        public IActionResult AddToCart(int addid)
        {
            var order = _context.Orders.Where(x => x.Status == "New").FirstOrDefault();
            if (order != null)
            {
                var product = _context.OrderItems.Where(x => x.ProductId == addid && x.OrderId == order.Id).FirstOrDefault();
                if (product != null)
                {
                    product.Count++;
                }
                else
                {
                    var item = new OrderItem(_context.Products.Find(addid), 1);
                    order.Items.Add(item);
                }
            }
            else
            {
                order = new Order();
                order.Status = "New";
                _context.Orders.Add(order);
                var item = new OrderItem(_context.Products.Find(addid), 1);
                order.Items.Add(item);
            }
            _context.SaveChanges();
            return RedirectToAction("index", "order");
        }

        public IActionResult ClearCart()
        {
            return null;
        }

    }
}
