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
        _assignCourierUseCase = assignCourierUseCase;
        _logger = logger;
    }

    [HttpPost("{deliveryId:guid}/assign-courier")]
    public async Task<IActionResult> AssignCourier(
        [FromRoute] Guid deliveryId,
        [FromBody] AssignCourierRequest request)
    {
        if (deliveryId == Guid.Empty || request.CourierId == Guid.Empty)
            return BadRequest(new { error = "Los identificadores de entrega y repartidor son obligatorios." });

        try
        {
            await _assignCourierUseCase.ExecuteAsync(deliveryId, request.CourierId);
            _logger.LogInformation(
                "Repartidor asignado correctamente a la entrega {DeliveryId}.",
                deliveryId);

            return Ok(new { deliveryId, courierId = request.CourierId, status = "Assigned" });
        }
        catch (KeyNotFoundException ex)
        {
            _logger.LogWarning(ex, "Entrega no encontrada al asignar repartidor: {DeliveryId}", deliveryId);
            return NotFound(new { error = ex.Message });
        }
        catch (InvalidOperationException ex)
        {
            _logger.LogWarning(ex, "Conflicto de regla de negocio en entrega {DeliveryId}", deliveryId);
            return Conflict(new { error = ex.Message });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error inesperado al asignar repartidor a la entrega {DeliveryId}", deliveryId);
            return StatusCode(StatusCodes.Status500InternalServerError,
                new { error = "Ocurrió un error interno al procesar la asignación." });
        }
    }
}