namespace Accounting.API.Exceptions.Transaction
{
    public class InvalidTransactionAdditionException : Exception
    {
        public InvalidTransactionAdditionException(string msg) : base(msg) { }
    }
}
