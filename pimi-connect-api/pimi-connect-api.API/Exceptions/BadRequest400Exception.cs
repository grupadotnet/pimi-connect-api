using pimi_connect_api.Exceptions.Base;

namespace pimi_connect_api.Exceptions;


public class BadRequest400Exception : CustomExceptionBase
{
    public override int StatusCode => 400;
    
    public BadRequest400Exception(string message) : base(message) { }
}