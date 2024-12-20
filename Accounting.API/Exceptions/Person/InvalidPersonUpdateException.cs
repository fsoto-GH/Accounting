namespace Accounting.API.Exceptions.Person
{
    public class InvalidPersonUpdateException : Exception
    {
        public InvalidPersonUpdateException(string msg) : base(msg) { }
    }
}
