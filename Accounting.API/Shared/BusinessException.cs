namespace Accounting.API.Shared
{
    public class BusinessException : Exception
    {
        public BusinessException(string message) : base(message) { }
    }
}
