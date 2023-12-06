namespace MVC.Practice.PustokMVC.Business.Exceptions.SliderExceptions
{
    public class InvalidImage : Exception
    {
        public string PropertyName { get; set; }
        public InvalidImage() { }
        public InvalidImage(string propertyName, string? message) : base(message)
        {

        }
    }
}
