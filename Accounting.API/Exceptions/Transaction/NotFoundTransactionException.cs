using System.Runtime.Serialization;

namespace Accounting.API.Exceptions.Transaction
{
    internal class NotFoundTransactionException : Exception
    {
        public NotFoundTransactionException(int personID, int accountID, int transactionID) : base($"Person/Account/Transaction ({personID}/{accountID}/{transactionID}) does not exist.") { }
    }
}