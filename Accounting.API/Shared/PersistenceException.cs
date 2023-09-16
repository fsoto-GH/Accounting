namespace Accounting.API.Shared
{
    public class PersistenceException : Exception
    {
        public PersistenceException(string message) : base(message) { }
    }
}
