namespace MVC.Practice.PustokMVC.Business.Exceptions.SliderExceptions
{
    public class InvalidNullReferance : Exception
    {
        public string PropertyName { get; set; }
        public InvalidNullReferance() { }
        public InvalidNullReferance(string? message) : base(message)
        {

        }
        public InvalidNullReferance(string propertyName, string? message) : base(message)
        {

        }
    }
}
