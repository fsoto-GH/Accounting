namespace Accounting.API.Exceptions.Transaction
{
    public class InvalidTransactionUpdateException : Exception
    {
        public InvalidTransactionUpdateException(string msg) : base(msg) { }
    }
}
