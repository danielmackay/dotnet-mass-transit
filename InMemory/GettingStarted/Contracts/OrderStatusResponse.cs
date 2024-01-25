using System;

namespace GettingStarted.Contracts;

public record OrderStatusResponse
{
    public string OrderId { get; init; }
    public DateTime Timestamp { get; init; }
    public short StatusCode { get; init; }
    public string StatusText { get; init; }
}
