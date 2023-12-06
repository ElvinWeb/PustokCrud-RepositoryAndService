namespace MVC.Practice.PustokMVC.Business.Exceptions.SliderExceptions
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
