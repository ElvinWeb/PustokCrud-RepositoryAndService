using MVC.Practice.PustokMVC.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PustokMVC.Core.Models
{
    public class OrderItem : BaseEntity
    {
        public int BookId { get; set; } 
        public Book? Book { get; set; }
        public int OrderId { get; set; }
        public Order? Order { get; set; }
        public string BookName { get; set; }
        public int Count { get; set; }
        public double SalePrice { get; set; }
        public double CostPrice { get; set; }
        public double DiscountPercent { get; set; }
    }
}
