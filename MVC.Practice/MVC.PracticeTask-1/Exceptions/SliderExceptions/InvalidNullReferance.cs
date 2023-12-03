namespace MVC.PracticeTask_1.Exceptions.SliderExceptions
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
