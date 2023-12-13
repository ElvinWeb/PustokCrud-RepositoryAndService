using PustokMVC.Core.Models;

namespace MVC.PracticeTask_1.ViewModel
{
    public class OrderViewModel
    {
        public List<CheckoutViewModel>? CheckoutViewModels { get; set; }
        public string FullName { get; set; }
        public string Country { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string ZipCode { get; set; }
        public string? Note { get; set; }

    }
}
