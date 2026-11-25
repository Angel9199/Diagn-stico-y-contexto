Nombre del producto: LogiFast

Problema principal: La empresa de retail digital recibe pedidos desde múltiples canales y maneja inventarios distribuidos, pero carece de una vista centralizada del estado real de cada pedido. Esto genera una alta duplicación de información, retrasos operativos, errores en las entregas y una completa pérdida de trazabilidad.

Usuarios (y Actores clave): Clientes finales, operadores de comercio/tienda, administradores de almacén, repartidores y agentes de soporte al cliente.

Objetivos:Centralizar la gestión de pedidos y el control de inventarios distribuidos en una única plataforma.   Optimizar la asignación de rutas y el rastreo de entregas de última milla.   Reducir las incidencias operativas, la duplicidad de datos y los tiempos de respuesta del servicio de soporte.

Alcance: El sistema abarcará desde la recepción centralizada de los pedidos y la validación del inventario hasta la asignación de rutas, el rastreo en tiempo real de la entrega y la gestión inicial de incidencias. Quedan excluidos en esta primera etapa los módulos avanzados de predicción de demanda mediante inteligencia artificial y la integración directa con flotas de transporte externas de terceros.

## Diagrama de Contexto

![Diagrama de Contexto del Sistema LogiFast][def]

# Actividad 2: Requisitos y Decisión Estructural - LogiFast

## Resumen de la Actividad
En esta segunda fase del proyecto **LogiFast**, transformamos las necesidades del negocio y los riesgos detectados en el diagnóstico inicial (Actividad 1) en requerimientos técnicos estructurados, criterios de calidad medibles y una decisión arquitectónica justificada que guiará el desarrollo e implementación del software.

---

## 1. Catálogo de Requisitos y Matriz de Priorización
Se definieron y priorizaron los requerimientos funcionales clave para la operación de última milla junto con los atributos no funcionales críticos:
* **RF01 (Recepción de Pedidos):** Unificación automatizada de órdenes multicanal.
* **RF02 (Control de Inventario):** Validación de stock en tiempo real en almacenes distribuidos (Prioridad Alta).
* **RF03 & RF04 (Rutas y Rastreo):** Asignación de rutas y actualización de estados en tiempo real para clientes y soporte.
* **RNF (Atributos de Calidad):** Alto enfoque en disponibilidad operativa, modificabilidad modular y seguridad de datos.

*(La matriz completa de priorización y evaluación de impacto se encuentra detallada en la documentación interna de la actividad).*

---

## 2. Escenarios de Calidad
Se establecieron escenarios con métricas concretas para evaluar el comportamiento del sistema:
* **Rendimiento:** Consultas de estado de pedidos en menos de 2 segundos.
* **Escalabilidad / Carga:** Soporte para al menos 1.000 transacciones por hora durante picos de demanda comercial.
* **Modificabilidad:** Cambios en reglas de negocio logísticas aislados y ejecutados sin refactorización total de la aplicación.
* **Seguridad:** Cifrado estricto de datos sensibles en tránsito y en reposo.

---

## 3. Decisión Arquitectónica (ADR-001)
* **Decisión Tomada:** Adopción de una arquitectura de **Monolito Modular con límites claros**.
* **Justificación:** Se evaluó frente a una arquitectura distribuida por microservicios. Se determinó que el monolito modular ofrece la velocidad de salida al mercado (*time-to-market*), la simplicidad transaccional y la baja complejidad operativa inicial ideales para la etapa actual del negocio, evitando un sobrediseño innecesario.
* **Trade-offs:** Si bien reduce costos y simplifica el despliegue, requiere una disciplina estricta de desarrollo (Code Reviews y separación de capas) para evitar el acoplamiento futuro entre módulos.

---

## 4. Criterios Verificables
La arquitectura seleccionada se evaluará objetivamente mediante:
1. **Aislamiento de Módulos:** Comprobación de que las dependencias cruzadas estén prohibidas y que modificar un dominio no afecte a los demás.
2. **Integridad Transaccional:** Garantizar que el bloqueo de inventario ante un pedido se ejecute de forma atómica.
3. **Despliegue Simplificado:** Comprobar que el sistema completo se compila e implementa mediante un único artefacto unificado.

# LogiFast — Actividad 3: Diseño y Dominio

Diseño del modelo de dominio, límites de contexto, casos de uso, puertos y adaptadores para el flujo de **asignación de repartidor a una entrega de última milla (`AssignCourier`)** sobre la plataforma **LogiFast**, dando continuidad a las Actividades 1 y 2.

---

## Alcance de esta actividad
* **Flujo modelado:** Asignación de un repartidor a una entrega (`AssignCourierUseCase`), validando estado, identificador y reglas de negocio antes de persistir[cite: 2, 4, 5].
* **Enfoque:** Separación estricta entre dominio, aplicación e infraestructura, con adaptador en memoria y pruebas unitarias de dominio[cite: 2, 4]. La persistencia productiva y las integraciones externas completas corresponden a la Actividad 4.

---

## Estructura del proyecto
* `docs/01-flujo-y-lenguaje-ubicuo.md`: Flujo de negocio, actores, glosario de lenguaje ubicuo y ambigüedades detectadas[cite: 2].
* `docs/Architecture.md`: Contextos delimitados, dirección de dependencias y diagramas C4 (contexto/contenedores y componentes del flujo)[cite: 2, 4].
* `docs/adr/`: Registro de decisión arquitectónica (`ADR-002`) sobre puertos, adaptadores y trade-offs[cite: 2].
* `src/LogiFast.Domain/`: Entidades (`Delivery`), objetos de valor (`DeliveryId`, `DeliveryAddress`), estados (`DeliveryStatus`) e invariantes[cite: 2, 4, 5].
* `src/LogiFast.Application/`: Puerto (`IDeliveryRepository`) y caso de uso (`AssignCourierUseCase`)[cite: 2, 4, 5].
* `src/LogiFast.Infrastructure/`: Adaptador en memoria (`InMemoryDeliveryRepository`)[cite: 2, 4].
* `tests/LogiFast.Domain.Tests/`: Pruebas unitarias que verifican las invariantes de `Delivery`[cite: 2, 4].

---

## Reglas e invariantes principales
* Una entrega en estado distinto a `Pending` o que ya tenga un repartidor asignado no puede recibir otro repartidor[cite: 3, 4, 5].
* El `courierId` asignado debe ser válido (distinto de `Guid.Empty`)[cite: 4, 5].
* `LogiFast.Domain` no depende de bases de datos, frameworks ni infraestructura externa.

---

## Escenarios de aceptación (Dado-Cuando-Entonces)
* **Dado** una entrega pendiente sin repartidor y un repartidor disponible, **Cuando** se ejecuta la asignación, **Entonces** la entrega queda en estado `Assigned` con el `courierId` correspondiente[cite: 2, 4, 5].
* **Dado** una entrega ya asignada a un repartidor, **Cuando** se intenta asignar otro repartidor, **Entonces** el dominio rechaza la operación y conserva la asignación original[cite: 3, 4, 5].

# LogiFast — Actividad 4: Implementación y Validación (`AssignCourier`)

Implementación ejecutable del flujo **Asignar repartidor** (`AssignCourier`) como *vertical slice* sobre una arquitectura de Monolito Modular con Puertos y Adaptadores en .NET.

## Estructura del proyecto
* `src/LogiFast.Domain`: Entidad `Delivery`, objetos de valor e invariantes de negocio.
* `src/LogiFast.Application`: Caso de uso `AssignCourierUseCase` y puerto `IDeliveryRepository`.
* `src/LogiFast.Infrastructure`: Adaptador `InMemoryDeliveryRepository`.
* `src/LogiFast.Api`: Endpoint `POST /api/deliveries/{deliveryId}/assign-courier`, DTO `AssignCourierRequest`, manejo de errores y registro de logs.
* `tests/LogiFast.Domain.Tests`: Pruebas unitarias de invariantes y comprobación automatizada de límites del dominio (`ArchitectureTests`).
* `tests/LogiFast.Integration.Tests`: Pruebas de integración del flujo (camino exitoso y rechazo por doble asignación).
* `docs/`: Expediente de diseño (diagramas, ADR, decisiones) y `riesgos-y-observabilidad.md`.

## Ejecución de pruebas

Desde la carpeta `docs/Actividad-3-4/Logifast`:

```powershell
dotnet test .\tests\LogiFast.Domain.Tests\LogiFast.Domain.Tests.csproj
dotnet test .\tests\LogiFast.Integration.Tests\LogiFast.Integration.Tests.csproj
