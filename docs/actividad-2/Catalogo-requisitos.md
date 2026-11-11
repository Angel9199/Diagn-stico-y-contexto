Catálogo de Requisitos - LogiFast (Actividad 2)
1. Requisitos Funcionales (RF)
RF01: Recepción y unificación de pedidos multicanal

Descripción: El sistema debe permitir registrar y centralizar de forma automatizada las órdenes de compra provenientes de múltiples canales digitales de venta, asignándoles un identificador único y un estado inicial ("Creado").

RF02: Validación y control de inventario distribuido

Descripción: El sistema debe verificar en tiempo real la disponibilidad de stock en el almacén o tienda física asignada antes de confirmar la orden, bloqueando o descontando unidades para evitar sobreventas o inconsistencias.

RF03: Asignación y planificación de rutas de última milla

Descripción: El sistema debe permitir asignar los pedidos validados a repartidores (propios o externos) y calcular/agrupar las rutas de entrega óptimas basándose en la ubicación del almacén y el destino del cliente.

RF04: Rastreo y actualización del estado del pedido en tiempo real

Descripción: El sistema debe proveer una interfaz para que los repartidores actualicen el estado del envío ("en ruta", "entregado", "con incidencia"), reflejándolo instantáneamente en la plataforma central para visibilidad de clientes y soporte.

RF05: Gestión de incidencias y trazabilidad de devoluciones

Descripción: El sistema debe registrar anomalías operativas (retrasos, fallos de entrega, rechazos de mercancía) y mantener un historial auditable accesible por el equipo de servicio al cliente.

RF06 Selección / Adición de Ventana de Entrega

Descripción: Permite especificar un rango horario de entrega sin alterar el contrato base de creación del pedido.

RNF07 Evolución de Contratos / Extensibilidad de la API

Descripción: Las APIs de creación de pedidos deben diseñarse de forma extensible (compatibilidad hacia atrás), permitiendo añadir atributos nuevos (como ventanas de entrega) sin romper clientes móviles existentes.

2. Atributos de Calidad (Requisitos No Funcionales - RNF)
RNF01: Rendimiento y tiempo de respuesta

Descripción: Las consultas de estado de pedidos realizadas por los clientes o agentes de soporte deben resolverse de forma inmediata bajo condiciones de operación normal.

RNF02: Disponibilidad y tolerancia a fallas

Descripción: La plataforma centralizada debe garantizar una alta disponibilidad operativa para evitar interrupciones en la recepción de pedidos y en las validaciones de inventario durante los picos de venta.

RNF03: Seguridad y confidencialidad de datos

Descripción: La información sensible de los clientes y las credenciales de acceso deben permanecer protegidas mediante protocolos de cifrado tanto en tránsito como en almacenamiento.

RNF04: Mantenibilidad y modificabilidad estructural

Descripción: Las reglas de negocio logísticas (como el cálculo de rutas o políticas de inventario) deben diseñarse de manera modular, permitiendo ajustes futuros sin afectar la integridad del resto del sistema.


## Matriz de Priorización

| Requisito | Tipo | Prioridad | Impacto en Negocio | Impacto en Arquitectura | Nivel de Complejidad |
| :--- | :--- | :--- | :--- | :--- | :--- |
| **RF01 (Recepción de Pedidos)** | Funcional | **Alta** | Crítico (Núcleo del negocio) | Alto (Define los límites de entrada) | Baja |
| **RF02 (Control de Inventario)** | Funcional | **Alta** | Crítico (Evita pérdidas y sobreventas) | Alto (Exige consistencia transaccional) | Media |
| **RF03 (Asignación de Rutas)** | Funcional | Media | Alto (Optimiza la última milla) | Media | Media |
| **RF04 (Rastreo en Tiempo Real)** | Funcional | Media | Alto (Mejora la experiencia del usuario) | Media | Media |
| **RF05 (Gestión de Incidencias)** | Funcional | Baja | Medio (Soporte y trazabilidad) | Baja | Baja |
| **RNF02 (Disponibilidad del Sistema)** | No Funcional | **Alta** | Crítico (Evita caídas en ventas) | Alto (Define infraestructura) | Alta |
| **RNF04 (Modificabilidad)** | No Funcional | Media | Medio (Facilita el crecimiento futuro) | Alto (Impone diseño modular) | Media |