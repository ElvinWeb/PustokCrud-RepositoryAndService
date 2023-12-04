namespace MVC.PracticeTask_1.Models
{
    public class BookImage : BaseEntity
    {
        public int BookId { get; set; }
        public string ImgUrl { get; set; }
        public bool? isPoster { get; set; }
        public Book? Book { get; set; }
    }
}
