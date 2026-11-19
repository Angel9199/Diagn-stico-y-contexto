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
        var deliveryId = new DeliveryId(deliveryIdValue);

        // 1. Cargar la entidad a través del puerto del repositorio
        var delivery = await _deliveryRepository.GetByIdAsync(deliveryId)
            ?? throw new InvalidOperationException($"No se encontró la entrega con ID '{deliveryIdValue}'.");

        // 2. Ejecutar la lógica de negocio (protege la invariante dentro del dominio)
        delivery.AssignCourier(courierId);

        // 3. Persistir los cambios mediante el puerto
        await _deliveryRepository.SaveAsync(delivery);
    }
}