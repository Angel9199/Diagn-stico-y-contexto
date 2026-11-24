using LogiFast.Domain.Entities;
using LogiFast.Domain.ValueObjects;

namespace LogiFast.Application.Ports;

public interface IDeliveryRepository
{
    Task<Delivery?> GetByIdAsync(DeliveryId id);
    Task SaveAsync(Delivery delivery);
}