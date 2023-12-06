namespace MVC.Practice.PustokMVC.Business.Exceptions.CommonModelsExceptions
{
    public class NotFound : Exception
    {
        public string PropertyName {  get; set; }
        public NotFound()
        {

        }

        public NotFound(string? message) : base(message)
        {

        }
        public NotFound(string propName ,string? message) : base(message)
        {
            PropertyName = propName;    
        }
    }
}
