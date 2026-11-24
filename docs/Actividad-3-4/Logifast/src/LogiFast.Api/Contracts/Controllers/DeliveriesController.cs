using LogiFast.Application.UseCases;
using Microsoft.AspNetCore.Mvc;

namespace LogiFast.Api.Controllers;

[ApiController]
[Route("api/deliveries")]
public sealed class DeliveriesController : ControllerBase
{
    private readonly AssignCourierUseCase _assignCourierUseCase;

    public DeliveriesController(AssignCourierUseCase assignCourierUseCase)
    {
        _assignCourierUseCase = assignCourierUseCase;
    }

    [HttpPost("{deliveryId:guid}/assign-courier")]
    public async Task<IActionResult> AssignCourier(
        [FromRoute] Guid deliveryId,
        [FromBody] AssignCourierRequest request)
    {
        if (request.CourierId == Guid.Empty)
            return BadRequest(new { error = "El identificador del repartidor es obligatorio." });

        try
        {
            await _assignCourierUseCase.ExecuteAsync(deliveryId, request.CourierId);
            return Ok(new { deliveryId, courierId = request.CourierId, status = "Assigned" });
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new { error = ex.Message });
        }
    }
}