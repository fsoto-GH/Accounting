namespace Accounting.API.Exceptions.Account
{
    public class NotFoundAccountException : Exception
    {
        public NotFoundAccountException(string msg) : base(msg) { }

        public NotFoundAccountException(int personID, int accountID) : base($"Person/Account ({personID}/{accountID}) does not exist.") { }
    }
}
