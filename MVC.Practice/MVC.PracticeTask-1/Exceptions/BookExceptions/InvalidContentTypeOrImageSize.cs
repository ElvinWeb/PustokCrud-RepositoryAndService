namespace MVC.PracticeTask_1.Exceptions.BookExceptions
{
    public class InvalidContentTypeOrImageSize : Exception
    {
        public string PropertyName { get; set; }
        public InvalidContentTypeOrImageSize() { }
        public InvalidContentTypeOrImageSize(string propertyName, string? message) : base(message)
        {

        }

    }
}
