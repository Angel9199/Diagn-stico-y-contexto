# Diseño de Dominio: Flujo de Negocio y Lenguaje Ubicuo

## 1. Selección del Flujo de Negocio
* **Nombre del Flujo:** Asignación de Repartidor a Entrega de Última Milla (`AssignCourier`).
* **Actores Involucrados:** 
  * *Operador de Comercio/Tienda* o *Administrador de Almacén* (inicia o supervisa la asignación).
  * *Sistema LogiFast* (orquesta validaciones y reglas de negocio).
  * *Repartidor* (recurso asignado para ejecutar el traslado).
* **Propósito:** Vincular de manera atómica y consistente una entrega lista para despacho con un repartidor activo que cuente con capacidad disponible en su ruta actual.
* **Resultado Esperado:** La entrega cambia su estado a `Asignada`, queda vinculada irrevocablemente a un `CourierId` hasta su cierre o liberación formal, se actualiza la carga del repartidor y se notifica el evento al sistema.

---

## 2. Glosario de Lenguaje Ubicuo (Ubiquitous Language)

| Término | Definición en el Dominio | Contexto Principal |
| :--- | :--- | :--- |
| **Pedido (Order)** | Solicitud comercial originada por un cliente final desde un canal de venta, que agrupa líneas de producto y una dirección de destino. | Pedidos (Ordering) |
| **Entrega (Delivery)** | Unidad operativa de transporte de última milla asociada a un pedido ya preparado, cuyo fin es llegar al cliente final. | Entregas (Delivery) |
| **Repartidor (Courier)** | Persona o unidad operativa encargada de transportar entregas desde un almacén hasta el destino final. | Entregas / Ruteo |
| **Ruta (Route)** | Secuencia planificada de entregas asignadas a un repartidor dentro de una ventana operativa. | Ruteo (Routing) |
| **Almacén (Warehouse)** | Punto físico de almacenamiento distribuido desde donde se despacha el inventario. | Inventario (Inventory) |
| **Invariante** | Regla de negocio inviolable que garantiza la consistencia del estado de una entidad en todo momento. | Transversal (Dominio) |

---

## 3. Términos Ambiguos Detectados (Para confirmar con Negocio)
De acuerdo con el análisis del caso obligatorio (*"Pedidos, Inventario, Ruteo y Entregas usan conceptos parecidos como disponibilidad, pero cada contexto tiene una regla distinta"*), se documentan las siguientes ambigüedades:

1. **"Disponibilidad" (Availability):**
   * *En Inventario:* Significa existencia física de unidades no reservadas en un almacén específico.
   * *En Ruteo/Entregas:* Significa que el repartidor está en turno activo y no ha alcanzado su límite máximo de paquetes o peso en su ruta.
   * *Resolución de diseño:* Se separan explícitamente en contratos distintos (`IInventoryAvailabilityPort` vs. `ICourierAvailabilityPort`).
2. **"Cancelado / Liberado":**
   * *Ambigüedad:* Si un repartidor sufre un imprevisto antes de salir del almacén, ¿la entrega se "Cancela" o se "Libera" para reasignación?
   * *Resolución de diseño:* La cancelación anula el pedido/entrega definitivamente; la desasignación devuelve la entrega a estado `Pendiente` para permitir un nuevo `AssignCourier`.