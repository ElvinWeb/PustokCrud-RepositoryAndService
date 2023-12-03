namespace MVC.PracticeTask_1.Exceptions.BookExceptions
{
    public class InvalidGenreId :Exception
    {
        public string PropertyName { get; set; }
        public InvalidGenreId() { }
        public InvalidGenreId(string propertyName, string? message) : base(message)
        {

        }
    }
}
