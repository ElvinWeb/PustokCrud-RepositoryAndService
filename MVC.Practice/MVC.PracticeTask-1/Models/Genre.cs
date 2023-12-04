namespace MVC.PracticeTask_1.Models
{
    public class Genre : BaseEntity
    {
        public string Name { get; set; }
        public List<Book>? Books { get; set; }
    }
}
