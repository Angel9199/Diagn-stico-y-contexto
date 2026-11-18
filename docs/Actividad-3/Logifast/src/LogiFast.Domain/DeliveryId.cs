namespace LogiFast.Domain.ValueObjects;

public sealed record DeliveryId(Guid Value)
{
    public static DeliveryId New() => new(Guid.NewGuid());
}