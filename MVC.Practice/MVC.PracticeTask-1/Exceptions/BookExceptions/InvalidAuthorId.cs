namespace MVC.PracticeTask_1.Exceptions.BookExceptions
{
    public class InvalidAuthorId : Exception
    {
        public string PropertyName { get; set; }
        public InvalidAuthorId() { }
        public InvalidAuthorId(string propertyName, string? message) : base(message)
        {

        }
    }
}
