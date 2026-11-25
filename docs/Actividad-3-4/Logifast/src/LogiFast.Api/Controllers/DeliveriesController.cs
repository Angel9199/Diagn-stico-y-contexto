using LogiFast.Api.Contracts;
using LogiFast.Application.UseCases;
using Microsoft.AspNetCore.Mvc;

namespace LogiFast.Api.Controllers;

[ApiController]
[Route("api/deliveries")]
public sealed class DeliveriesController : ControllerBase
{
    private readonly AssignCourierUseCase _assignCourierUseCase;
    private readonly ILogger<DeliveriesController> _logger;

    public DeliveriesController(
        AssignCourierUseCase assignCourierUseCase,
        ILogger<DeliveriesController> logger)
    {
        _assignCourierUseCase = assignCourierUseCase ?? throw new ArgumentNullException(nameof(assignCourierUseCase));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    [HttpPost("{deliveryId:guid}/assign-courier")]
    public async Task<IActionResult> AssignCourier(
        [FromRoute] Guid deliveryId,
        [FromBody] AssignCourierRequest request)
    {
        if (deliveryId == Guid.Empty || request is null || request.CourierId == Guid.Empty)
        {
            _logger.LogWarning("Petición inválida para asignar repartidor en DeliveryId {DeliveryId}.", deliveryId);
            return BadRequest(new { message = "El identificador de la entrega y del repartidor son obligatorios." });
        }

        try
        {
            await _assignCourierUseCase.ExecuteAsync(deliveryId, request.CourierId);

            _logger.LogInformation("Repartidor asignado correctamente en DeliveryId {DeliveryId}. Estado: Assigned.", deliveryId);
            return Ok(new { deliveryId, status = "Assigned" });
        }
        catch (KeyNotFoundException ex)
        {
            _logger.LogWarning(ex, "Entrega no encontrada para DeliveryId {DeliveryId}.", deliveryId);
            return NotFound(new { message = "La entrega solicitada no existe." });
        }
        catch (InvalidOperationException ex)
        {
            _logger.LogWarning(ex, "Conflicto de estado al asignar repartidor en DeliveryId {DeliveryId}.", deliveryId);
            return Conflict(new { message = ex.Message });
        }
        catch (ArgumentException ex)
        {
            _logger.LogWarning(ex, "Argumento inválido en DeliveryId {DeliveryId}.", deliveryId);
            return BadRequest(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error interno inesperado al procesar DeliveryId {DeliveryId}.", deliveryId);
            return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Ocurrió un error interno al procesar la solicitud." });
        }
    }
}