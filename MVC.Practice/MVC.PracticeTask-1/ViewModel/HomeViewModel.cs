using MVC.Practice.PustokMVC.Core.Models;

namespace MVC.PracticeTask_1.ViewModel
{
    public class HomeViewModel
    {
        public List<Slide> Sliders { get; set; }
        public List<Book> FeaturedBooks { get; set; }
        public List<Book> NewBooks { get; set; }
        public List<Book> BestsellerBooks { get; set; }
    }
}
