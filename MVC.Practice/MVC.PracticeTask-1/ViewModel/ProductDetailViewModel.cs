using MVC.Practice.PustokMVC.Core.Models;

namespace MVC.PracticeTask_1.ViewModel
{
    public class ProductDetailViewModel
    {
        public Book Book { get; set; }
        public List<Book> RelatedBooks { get; set; }
    }
}
