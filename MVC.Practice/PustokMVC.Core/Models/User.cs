using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PustokMVC.Core.Models
{
    public class User : IdentityUser
    {
        public string FullName { get; set; }
        public string BirthDate { get; set; }
        public string? ConnectionId { get; set; }
        public List<BasketItem> BasketItems { get; set; }
        public List<Order> Orders { get; set; }

    }
}
