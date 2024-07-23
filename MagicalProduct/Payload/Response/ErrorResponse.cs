﻿namespace MagicalProduct.API.Payload.Response;

using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;

public class ErrorResponse
{
    public bool IsSuccess { get; set; } = false;
    public int StatusCode { get; set; }
    public string Message { get; set; }
    public object? Result { get; set; }

    public override string ToString()
	{
        return JsonConvert.SerializeObject(this, new JsonSerializerSettings
        {
            ContractResolver = new DefaultContractResolver
            {
                NamingStrategy = new KebabCaseNamingStrategy()
            },
            Formatting = Formatting.Indented
        });
    }
}