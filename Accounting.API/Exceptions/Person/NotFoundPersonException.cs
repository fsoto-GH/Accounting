namespace Accounting.API.Exceptions.Person
{
    public class NotFoundPersonException : Exception
    {
        public NotFoundPersonException(string msg) : base(msg) { }

        public NotFoundPersonException(int personID) : base($"Person ({personID}) does not exist.") { }
    }
}
