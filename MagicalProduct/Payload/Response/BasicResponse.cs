namespace MagicalProduct.API.Payload.Response;

using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;

public class BasicResponse
{
    public bool IsSuccess { get; set; }
    public int StatusCode { get; set; }
    public string Message { get; set; }
    public object? Result { get; set; }

}