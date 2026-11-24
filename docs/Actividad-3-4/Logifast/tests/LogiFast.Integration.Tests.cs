using LogiFast.Application.UseCases;
using LogiFast.Domain.Entities;
using LogiFast.Domain.Enums;
using LogiFast.Domain.ValueObjects;
using LogiFast.Infrastructure.Repositories;
using Xunit;

namespace LogiFast.Integration.Tests;

public class AssignCourierIntegrationTests
{
    [Fact]
    public async Task ExecuteAsync_HappyPath_ShouldAssignCourierAndPersistUpdatedDelivery()
    {
        var repository = new InMemoryDeliveryRepository();
        var useCase = new AssignCourierUseCase(repository);

        var deliveryId = DeliveryId.New();
        var address = new DeliveryAddress("Carrera 43A # 1-50", "Medellín", 6.2005, -75.5711);
        var delivery = new Delivery(deliveryId, address);
        await repository.SaveAsync(delivery);

        var courierId = Guid.NewGuid();

        await useCase.ExecuteAsync(deliveryId.Value, courierId);

        var savedDelivery = await repository.GetByIdAsync(deliveryId);
        Assert.NotNull(savedDelivery);
        Assert.Equal(DeliveryStatus.Assigned, savedDelivery!.Status);
        Assert.Equal(courierId, savedDelivery.CourierId);
    }

    [Fact]
    public async Task ExecuteAsync_WhenDeliveryIsAlreadyAssigned_ShouldRejectSecondAssignment()
    {
        var repository = new InMemoryDeliveryRepository();
        var useCase = new AssignCourierUseCase(repository);

        var deliveryId = DeliveryId.New();
        var address = new DeliveryAddress("Calle 50 # 70-10", "Medellín", 6.2518, -75.5636);
        var delivery = new Delivery(deliveryId, address);
        var firstCourier = Guid.NewGuid();
        delivery.AssignCourier(firstCourier);
        await repository.SaveAsync(delivery);

        var secondCourier = Guid.NewGuid();

        await Assert.ThrowsAsync<InvalidOperationException>(() =>
            useCase.ExecuteAsync(deliveryId.Value, secondCourier));

        var savedDelivery = await repository.GetByIdAsync(deliveryId);
        Assert.Equal(firstCourier, savedDelivery!.CourierId);
    }
}