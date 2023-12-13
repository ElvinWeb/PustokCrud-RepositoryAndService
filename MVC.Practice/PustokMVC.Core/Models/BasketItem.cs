using MVC.Practice.PustokMVC.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PustokMVC.Core.Models
{
    public class BasketItem : BaseEntity
    {
        public string UserId { get; set; }
        public int BookId { get; set; }
        public int Count { get; set; }
        public Book Book { get; set; }
        public User User { get; set; }
    }
}
