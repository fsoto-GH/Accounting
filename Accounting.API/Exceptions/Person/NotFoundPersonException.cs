namespace Accounting.API.Exceptions.Person
{
    public class NotFoundPersonException : Exception
    {
        public NotFoundPersonException(string msg) : base(msg) { }
    }
}
