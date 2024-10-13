namespace No_Overspend_Api.HttpExceptions
{
    public class UnauthorizeException : Exception
    {
        public UnauthorizeException() { }
        public UnauthorizeException(string message) : base(message) { }
        public UnauthorizeException(string message, Exception inner) : base(message, inner) { }
    }
}
