# ADR-002: Adopción de Puertos y Adaptadores (Arquitectura Limpia) para el Flujo de Entregas

* **Estado:** Aceptado
* **Fecha:** 2026-11
* **Contexto:** En la Actividad 2 se definió un Monolito Modular para LogiFast. Ahora necesitamos modelar el flujo de asignación de entregas (`AssignCourier`) de forma que las reglas e invariantes de negocio no dependan de bases de datos, frameworks ni servicios externos, y que cada contexto mantenga su propia semántica (por ejemplo, "disponibilidad" en entregas vs. inventario o ruteo).
* **Alternativas consideradas:**
  1. **Lógica de negocio en servicios de infraestructura / controladores o acceso directo a ORM en casos de uso:** más rápido al inicio, pero acopla las reglas de negocio a la persistencia o al transporte y dificulta probar invariantes de forma aislada.
  2. **Puertos y adaptadores (Arquitectura Limpia / Hexagonal):** el dominio concentra las entidades, objetos de valor e invariantes; la aplicación define casos de uso y puertos (`IDeliveryRepository`); la infraestructura implementa los adaptadores concretos.
* **Decisión:** Adoptar la alternativa 2 (**Puertos y Adaptadores**), manteniendo `LogiFast.Domain` sin dependencias externas y haciendo que `LogiFast.Application` dependa solo de `LogiFast.Domain`, mientras `LogiFast.Infrastructure` implementa los puertos.
* **Trade-offs:** Requiere más archivos e interfaces iniciales y disciplina en las referencias entre proyectos, pero protege las invariantes del dominio, facilita pruebas unitarias sin base de datos y permite cambiar el adaptador de persistencia o integración sin reescribir las reglas de negocio.