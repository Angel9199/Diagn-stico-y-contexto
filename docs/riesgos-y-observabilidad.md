# Riesgos, Observabilidad y Evolución — Flujo `AssignCourier` (LogiFast)

## 1. Matriz de Riesgos Operativos y de Seguridad

| Tipo | Riesgo | Impacto en el flujo | Mitigación implementada | Evidencia de verificación |
| :--- | :--- | :--- | :--- | :--- |
| **Operativo** | Doble asignación concurrente o reasignación de una entrega ya tomada por un repartidor | Dos repartidores acuden por la misma entrega o se pierde la trazabilidad de quién la atiende | Invariante protegida en `Delivery.AssignCourier`: bloquea cualquier asignación si el estado no es `Pending` o si ya existe un `CourierId`, devolviendo `409 Conflict` en la capa API | Prueba unitaria `AssignCourier_WhenDeliveryAlreadyHasCourier_ShouldThrowInvalidOperationException` y prueba de integración `ExecuteAsync_WhenDeliveryIsAlreadyAssigned_ShouldRejectSecondAssignment` |
| **Operativo** | Pérdida de estado al reiniciar el servicio por uso de almacenamiento en memoria | Al emplear `InMemoryDeliveryRepository` en este vertical slice, los datos de entregas no persisten entre reinicios | Desacoplamiento mediante el puerto `IDeliveryRepository` en `LogiFast.Application`, permitiendo migrar a persistencia durable sin modificar dominio ni casos de uso | Pruebas de integración ejecutadas contra `InMemoryDeliveryRepository` y registro en riesgos residuales |
| **Operativo** | Solicitud sobre una entrega inexistente | Respuesta ambigua o fallo no controlado al consultar una entrega que no existe | Validación explícita en `AssignCourierUseCase` lanzando `KeyNotFoundException` y mapeo controlado a `404 Not Found` en `DeliveriesController` | Manejo explícito en `AssignCourierUseCase.cs` y `DeliveriesController.cs` |
| **Seguridad** | Exposición de detalles internos (`StackTrace`, rutas de servidor o PII) en errores o registros | Fuga de información interna de infraestructura o datos personales de clientes y repartidores | Captura centralizada en `DeliveriesController` devolviendo respuestas controladas (`400`, `404`, `409`, `500` genérico sin traza) y registros acotados exclusivamente a `DeliveryId` | Revisión de `DeliveriesController.cs` y verificación de mensajes de error y logs |
| **Seguridad** | Identificadores inválidos o vacíos (`Guid.Empty`) en la petición de entrada | Creación de estados inconsistentes o errores no controlados | Validación temprana en `DeliveriesController` (`400 Bad Request`) y validación defensiva en `Delivery.AssignCourier` (`ArgumentException`) | Prueba unitaria `AssignCourier_WhenCourierIdIsEmpty_ShouldThrowArgumentException` |

---

## 2. Señales de Observabilidad

Para diagnosticar el comportamiento del flujo `POST /api/deliveries/{deliveryId}/assign-courier` sin comprometer la privacidad ni la seguridad:

### Registros (Logs estructurados)
* **Nivel `Information`:** Registro de asignación exitosa indicando únicamente `DeliveryId` y el cambio de estado a `Assigned`.
* **Nivel `Warning`:** Registro de rechazos controlados por reglas de negocio (`409 Conflict` por entrega ya asignada) o recurso inexistente (`404 Not Found`), incluyendo `DeliveryId` y motivo técnico.
* **Nivel `Error`:** Registro de fallos inesperados con detalle técnico interno en servidor, devolviendo al cliente un mensaje genérico (`500 Internal Server Error`).
* **Política de exclusión de datos sensibles:** Queda estrictamente excluido registrar direcciones físicas de entrega, nombres, teléfonos, correos, credenciales o tokens de autenticación 