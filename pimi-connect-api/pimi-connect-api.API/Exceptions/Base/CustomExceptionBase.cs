namespace pimi_connect_api.Exceptions.Base;

public abstract class CustomExceptionBase : Exception
{
    public abstract int StatusCode { get; }
    public string UserMessage { get; set; }
    
    protected CustomExceptionBase(string message)
    {
        UserMessage = $"Error {StatusCode}. {message}";
    }
}