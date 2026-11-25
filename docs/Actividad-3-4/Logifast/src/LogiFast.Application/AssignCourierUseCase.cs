using LogiFast.Application.Ports;
using LogiFast.Domain.ValueObjects;

namespace LogiFast.Application.UseCases;

public sealed class AssignCourierUseCase
{
    private readonly IDeliveryRepository _deliveryRepository;

    public AssignCourierUseCase(IDeliveryRepository deliveryRepository)
    {
        _deliveryRepository = deliveryRepository ?? throw new ArgumentNullException(nameof(deliveryRepository));
    }

    public async Task ExecuteAsync(Guid deliveryIdValue, Guid courierId)
    {
        if (deliveryIdValue == Guid.Empty)
            throw new ArgumentException("El ID de la entrega es obligatorio.", nameof(deliveryIdValue));

        var deliveryId = new DeliveryId(deliveryIdValue);

        var delivery = await _deliveryRepository.GetByIdAsync(deliveryId)
            ?? throw new KeyNotFoundException($"No se encontró la entrega con ID '{deliveryIdValue}'.");

        delivery.AssignCourier(courierId);

        await _deliveryRepository.SaveAsync(delivery);
    }
}