using System.Collections.Concurrent;
using LogiFast.Application.Ports;
using LogiFast.Domain.Entities;
using LogiFast.Domain.ValueObjects;

namespace LogiFast.Infrastructure.Repositories;

public sealed class InMemoryDeliveryRepository : IDeliveryRepository
{
    private readonly ConcurrentDictionary<DeliveryId, Delivery> _deliveries = new();

    public Task<Delivery?> GetByIdAsync(DeliveryId id)
    {
        _deliveries.TryGetValue(id, out var delivery);
        return Task.FromResult(delivery);
    }

    public Task SaveAsync(Delivery delivery)
    {
        if (delivery is null)
            throw new ArgumentNullException(nameof(delivery));

        _deliveries[delivery.Id] = delivery;
        return Task.CompletedTask;
    }
}