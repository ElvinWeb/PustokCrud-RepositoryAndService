namespace MVC.Practice.PustokMVC.Business.Exceptions.SliderExceptions
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
