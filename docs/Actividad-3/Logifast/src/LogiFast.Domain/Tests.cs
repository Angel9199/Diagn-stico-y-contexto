using LogiFast.Domain.Entities;
using LogiFast.Domain.Enums;
using LogiFast.Domain.ValueObjects;
using Xunit;

namespace LogiFast.Domain.Tests;

public class DeliveryTests
{
    private static Delivery CreatePendingDelivery()
    {
        var address = new DeliveryAddress("Calle 10 # 43-20", "Medellín", 6.2088, -75.5659);
        return new Delivery(DeliveryId.New(), address);
    }

    [Fact]
    public void AssignCourier_WhenDeliveryIsPending_ShouldAssignAndChangeStatusToAssigned()
    {
        var delivery = CreatePendingDelivery();
        var courierId = Guid.NewGuid();

        delivery.AssignCourier(courierId);

        Assert.Equal(courierId, delivery.CourierId);
        Assert.Equal(DeliveryStatus.Assigned, delivery.Status);
    }

    [Fact]
    public void AssignCourier_WhenDeliveryAlreadyHasCourier_ShouldThrowInvalidOperationException()
    {
        var delivery = CreatePendingDelivery();
        var firstCourier = Guid.NewGuid();
        var secondCourier = Guid.NewGuid();
        delivery.AssignCourier(firstCourier);

        Assert.Throws<InvalidOperationException>(() => delivery.AssignCourier(secondCourier));
        Assert.Equal(firstCourier, delivery.CourierId);
    }
}