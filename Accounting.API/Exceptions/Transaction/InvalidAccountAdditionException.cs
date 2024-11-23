namespace Accounting.API.Exceptions.Transaction
{
    public class InvalidAccountAdditionException : Exception
    {
        public InvalidAccountAdditionException(string msg) : base(msg) { }
    }
}
