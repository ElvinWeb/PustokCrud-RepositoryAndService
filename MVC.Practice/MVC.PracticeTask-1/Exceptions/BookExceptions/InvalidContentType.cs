namespace MVC.PracticeTask_1.Exceptions.BookExceptions
{
    public class InvalidContentType : Exception
    {
        public string PropertyName { get; set; }
        public InvalidContentType() { }
        public InvalidContentType(string propertyName, string? message) : base(message)
        {

        }

    }
}
