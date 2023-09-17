namespace Accounting.API.Exceptions.Person
{
    public class InvalidPersonDeletionException : Exception
    {
        public InvalidPersonDeletionException(string msg) : base(msg) { }
    }
}
