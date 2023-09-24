namespace Accounting.API.Exceptions.Account
{
    public class InvalidAccountUpdateException : Exception
    {
        public InvalidAccountUpdateException(string msg) : base(msg) { }

        public InvalidAccountUpdateException(int personID, int accountID) : base($"Person/Account ({personID}/{accountID}) has a balance, so it can not be closed.") { }
    }
}
