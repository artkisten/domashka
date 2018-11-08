using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shop.Data;
using Shop.Models;

namespace Shop.ViewModel
{
    public class OrderViewModel
    {
        public string Number { get; set; }
        public decimal TotalSum => Items.Sum(x => x.TotalSum);
        public List<OrderItemViewModel> Items { get; set; }
    }
    public class OrderItemViewModel
    {
        public int? ProductId;
        public decimal TotalSum => ProductPrice * Count;
        public string ProductName { get; set; }
        public int Count { get; set; }
        public decimal ProductPrice { get; set; }
    }
}