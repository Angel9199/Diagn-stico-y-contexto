using System.Collections.Concurrent;
using LogiFast.Application.Ports;
using LogiFast.Domain.Entities;
using LogiFast.Domain.ValueObjects;

namespace LogiFast.Infrastructure.Repositories;

public sealed class InMemoryDeliveryRepository : IDeliveryRepository
{
    private readonly ConcurrentDictionary<Guid, Delivery> _storage = new();

    public Task<Delivery?> GetByIdAsync(DeliveryId id)
    {
        _storage.TryGetValue(id.Value, out var delivery);
        return Task.FromResult(delivery);
    }

    public Task SaveAsync(Delivery delivery)
    {
        _storage[delivery.Id.Value] = delivery;
        return Task.CompletedTask;
    }
}