using System;
using System.Text.Json;

namespace DAPR.Test.Frontend.Extensions;

public class ErrorDetails
{
    public int StatusCode { get; set; }
    public string? Message { get; set; }
    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}

