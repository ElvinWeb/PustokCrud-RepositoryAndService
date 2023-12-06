
namespace MVC.Practice.PustokMVC.Business.Exceptions.CommonModelsExceptions
{

    public class InvalidAlreadyCreated : Exception
    {
        public string PropertyName { get; set; }
        public InvalidAlreadyCreated()
        {
        }

        public InvalidAlreadyCreated(string? message) : base(message)
        {
        }

        public InvalidAlreadyCreated(string propName, string? message) : base(message)
        {
            PropertyName = propName;
        }


    }
}
