namespace Basket.Application.CustomException
{
    public class NotFoundException : ArgumentNullException
    {
        public NotFoundException(string name)
            : base($"Entity \"{name}\" not found.") { }
    }
}
