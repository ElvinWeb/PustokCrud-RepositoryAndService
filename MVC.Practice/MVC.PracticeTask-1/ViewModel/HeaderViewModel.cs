using MVC.Practice.PustokMVC.Core.Models;
using PustokMVC.Core.Models;

namespace MVC.PracticeTask_1.ViewModel
{
    public class HeaderViewModel
    {
        public List<Genre> Genres { get; set; }
        public List<Setting> Settings { get; set; }
        public User User { get; set; }  
    }
}
