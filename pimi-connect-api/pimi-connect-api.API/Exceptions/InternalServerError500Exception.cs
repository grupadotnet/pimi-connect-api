using pimi_connect_api.Exceptions.Base;

namespace pimi_connect_api.Exceptions
{
    public class InternalServerError500Exception : CustomExceptionBase
    {
        public override int StatusCode => 500;

        public InternalServerError500Exception(string message) : base(message) { }
    }
}
