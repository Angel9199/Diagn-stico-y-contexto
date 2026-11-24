using LogiFast.Domain.ValueObjects;
using LogiFast.Domain.Enums;

namespace LogiFast.Domain.Entities;

public sealed class Delivery
{
    public DeliveryId Id { get; }
    public DeliveryAddress Address { get; }
    public DeliveryStatus Status { get; private set; }
    public Guid? CourierId { get; private set; }

    public Delivery(DeliveryId id, DeliveryAddress address)
    {
        Id = id ?? throw new ArgumentNullException(nameof(id));
        Address = address ?? throw new ArgumentNullException(nameof(address));
        Status = DeliveryStatus.Pending;
        CourierId = null;
    }

    // Regla de Negocio / Invariante: Asignación de repartidor
    public void AssignCourier(Guid courierId)
    {
        if (courierId == Guid.Empty)
            throw new ArgumentException("El ID del repartidor no es válido.", nameof(courierId));

        if (Status != DeliveryStatus.Pending)
            throw new InvalidOperationException($"No se puede asignar un repartidor a una entrega que se encuentra en estado '{Status}'.");

        if (CourierId.HasValue)
            throw new InvalidOperationException("Esta entrega ya tiene un repartidor asignado.");

        CourierId = courierId;
        Status = DeliveryStatus.Assigned;
    }

    // Liberar entrega si el repartidor tiene un imprevisto
    public void UnassignCourier()
    {
        if (Status != DeliveryStatus.Assigned)
            throw new InvalidOperationException("Solo se pueden desasignar entregas que estén en estado 'Asignada'.");

        CourierId = null;
        Status = DeliveryStatus.Pending;
    }
}