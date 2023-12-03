namespace MVC.PracticeTask_1.Exceptions.BookExceptions
{
    public class InvalidImageSize : Exception
    {
        public string PropertyName { get; set; }
        public InvalidImageSize() { }
        public InvalidImageSize(string propertyName, string? message) : base(message)
        {

        }
    }
}
