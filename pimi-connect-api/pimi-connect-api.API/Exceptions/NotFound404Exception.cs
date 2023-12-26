using pimi_connect_api.Exceptions.Base;

namespace pimi_connect_api.Exceptions;

public class NotFound404Exception : CustomExceptionBase
{
    public override int StatusCode => 404;
    
    // For custom message
    public NotFound404Exception(string message) : base(message) { }
    
    // For generic message
    public NotFound404Exception(string assetName, string id) : base(GetGenericMessage(assetName, id)) { }
    
    private static string GetGenericMessage(string assetName, string id)
    {
        return $"{assetName} with id {id} was not found.";
    }
}