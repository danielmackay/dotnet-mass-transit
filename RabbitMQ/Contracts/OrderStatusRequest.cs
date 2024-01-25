namespace Contracts;

public record OrderStatusRequest
{
    public string OrderId { get; init; }
}
