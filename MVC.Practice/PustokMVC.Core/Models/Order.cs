using MVC.Practice.PustokMVC.Core.Models;
using PustokMVC.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PustokMVC.Core.Models
{
    public class Order : BaseEntity
    {
        public string FullName { get; set; }
        public string Country { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string ZipCode { get; set; }
        public string? Note { get; set; }
        public string? AdminComment { get; set; }
        public double TotalPrice { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public User? User { get; set; }
        public string? UserId { get; set; }
        public List<OrderItem> OrderItems { get; set; }
    }
}
