namespace Catalog.Application.Exception
{
    public class ArgumentIdException : ArgumentException
    {
        public ArgumentIdException(string message)
            : base(message) { }
    }
}
