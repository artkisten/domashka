using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shop.Data;
using Microsoft.Extensions.Configuration;
using Shop.Models;
using Microsoft.Extensions.DependencyInjection;
using Shop.ViewModel;

namespace Shop.Controllers
{
    public class ProductController : Controller
    {
        //public IConfiguration Configuration { get; }
        private readonly ShopContext db;
        public ProductController(ShopContext context)
        {
            db = context;
            //Configuration = configuration;
            //var optionBuilder = new DbContextOptionsBuilder();
            //optionBuilder.UseSqlServer(Configuration.GetConnectionString("ShopContext"));
            //db = new ShopContext(optionBuilder.Options);
        }
        public IActionResult Index()
        {
            return View(db.Products.ToList());
        } 
        public IActionResult Add(Product product)
        {                      
            db.Products.Add(product);
            db.SaveChanges();
            return Redirect("index");
        }
        public IActionResult AddProduct()
        {
            return View();
        }
        public IActionResult Delete(int deleteid)
        {
            var prod = db.Products.First(x => x.Id == deleteid);
            db.Products.Remove(prod);
            db.SaveChanges();
            return Redirect("index");
        }
    }
}