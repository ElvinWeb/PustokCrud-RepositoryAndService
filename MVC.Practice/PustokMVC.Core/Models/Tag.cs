namespace MVC.Practice.PustokMVC.Core.Models
{
    public class Tag : BaseEntity
    {
        public string Name { get; set; }
        public List<BookTag>? BookTags { get; set; }
    }
}
