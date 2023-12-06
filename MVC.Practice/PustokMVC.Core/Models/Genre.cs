namespace MVC.Practice.PustokMVC.Core.Models
{
    public class Genre : BaseEntity
    {
        public string Name { get; set; }
        public List<Book>? Books { get; set; }
    }
}
