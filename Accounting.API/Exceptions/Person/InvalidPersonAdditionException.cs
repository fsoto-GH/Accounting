namespace Accounting.API.Exceptions.Person
{
    public class InvalidPersonAdditionException : Exception
    {
        public InvalidPersonAdditionException(string msg) : base(msg) { }
    }
}
