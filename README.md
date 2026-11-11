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

