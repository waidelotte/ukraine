namespace Ukraine.Domain.Exceptions
{
    public class CoreException : Exception
    {
        public CoreException(string message) : base(message) { }

        public static CoreException Exception(string message)
        {
            return new CoreException(message);
        }

        public static CoreException NullOrEmpty(string paramName)
        {
            return Exception($"{paramName} cannot be null or empty");
        }
    }
}