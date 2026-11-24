namespace LogiFast.Domain.ValueObjects;

public sealed record DeliveryAddress(string Street, string City, double Latitude, double Longitude)
{
    public string FullAddress => $"{Street}, {City}";
}