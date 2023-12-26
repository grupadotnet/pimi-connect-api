using pimi_connect_api.Exceptions.Base;

namespace pimi_connect_api.Exceptions;

public class Unauthorized401Exception: CustomExceptionBase
{
    public override int StatusCode => 401;
    
    public Unauthorized401Exception(string message) : base(message) { }
}