using LogiFast.Application.UseCases;
using LogiFast.Domain.Entities;
using LogiFast.Domain.Enums;
using LogiFast.Domain.ValueObjects;
using LogiFast.Infrastructure.Repositories;
using Xunit;

namespace LogiFast.Integration.Tests;

public sealed class AssignCourierIntegrationTests
{
    [Fact]
    public async Task ExecuteAsync_HappyPath_ShouldAssignCourierAndPersistDelivery()
    {
        var repository = new InMemoryDeliveryRepository();
        var useCase = new AssignCourierUseCase(repository);

        var deliveryId = DeliveryId.New();
        var address = new DeliveryAddress("Carrera 43A # 1-50", "Medellín", 6.2088, -75.5670);
        var delivery = new Delivery(deliveryId, address);
        await repository.SaveAsync(delivery);

        var courierId = Guid.NewGuid();
        await useCase.ExecuteAsync(deliveryId.Value, courierId);

        var savedDelivery = await repository.GetByIdAsync(deliveryId);
        Assert.NotNull(savedDelivery);
        Assert.Equal(courierId, savedDelivery!.CourierId);
        Assert.Equal(DeliveryStatus.Assigned, savedDelivery.Status);
    }

    [Fact]
    public async Task ExecuteAsync_WhenAlreadyAssigned_ShouldThrowInvalidOperationException()
    {
        var repository = new InMemoryDeliveryRepository();
        var useCase = new AssignCourierUseCase(repository);

        var deliveryId = DeliveryId.New();
        var address = new DeliveryAddress("Carrera 43A # 1-50", "Medellín", 6.2088, -75.5670);
        var delivery = new Delivery(deliveryId, address);
        await repository.SaveAsync(delivery);

        await useCase.ExecuteAsync(deliveryId.Value, Guid.NewGuid());

        await Assert.ThrowsAsync<InvalidOperationException>(
            () => useCase.ExecuteAsync(deliveryId.Value, Guid.NewGuid()));
    }
}