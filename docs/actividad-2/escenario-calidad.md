Escenarios de Calidad - LogiFast
1. Escenario de Rendimiento (Consultas y Trazabilidad)
Estímulo: Un cliente final o un agente de soporte realiza una consulta desde la aplicación móvil o web para verificar el estado en tiempo real y la ubicación de un pedido activo.

Entorno: Operación en horario de alta concurrencia normal.

Respuesta: El sistema procesa la solicitud consultando la base de datos centralizada y devuelve la información actualizada a la interfaz de usuario.

Medida (Métrica): La respuesta completa de la consulta debe entregarse en menos de 2 segundos.

2. Escenario de Escalabilidad / Carga (Picos de Demanda)
Estímulo: Se presenta un evento de alto tráfico comercial (ej. campañas de descuentos masivos o temporada navideña) donde ingresan pedidos simultáneos desde múltiples canales digitales.

Entorno: Entorno de producción bajo alta concurrencia imprevista.

Respuesta: El sistema gestiona las solicitudes de recepción de pedidos y validación de inventario distribuidos sin bloquear los hilos principales de ejecución.

Medida (Métrica): La plataforma debe soportar al menos 1.000 transacciones por hora manteniendo una tasa de errores inferior al 0.5% y sin degradación crítica en los tiempos de respuesta.

3. Escenario de Modificabilidad (Extensibilidad del Contrato de Pedidos):

Estímulo: El equipo añade el atributo de "ventana de entrega" (rango horario) a la estructura del pedido desde la app móvil.

Entorno: Evolución del módulo de pedidos en producción.

Respuesta: El contrato de la API procesa el nuevo campo de forma opcional/extensible sin requerir una versión destructiva de la API ni romper las versiones de la aplicación móvil que no envíen dicho campo.

Medida: 0 errores de integración o caídas en la aplicación móvil tras incorporar la nueva propiedad.

4. Escenario de Seguridad (Confidencialidad de Datos)
Estímulo: Un actor malintencionado intenta interceptar el tráfico de red o acceder directamente a las tablas de la base de datos que contienen información personal de los clientes (direcciones, teléfonos) y accesos.

Entorno: Tráfico en redes públicas de internet y almacenamiento en servidores.

Respuesta: El sistema bloquea accesos no autorizados mediante políticas estrictas de control y mantiene los datos cifrados.

Medida (Métrica): Toda la información sensible debe permanecer cifrada tanto en tránsito (usando HTTPS/TLS) como en reposo en la base de datos, cumpliendo con los estándares básicos de protección de datos.

5. Escenario de Escalabilidad (Picos de Campaña):

Estímulo: Se lanza una campaña comercial de alto impacto que genera un pico repentino de tráfico y solicitudes de creación de pedidos.

Entorno: Producción bajo concurrencia masiva e imprevista.

Respuesta: La arquitectura del monolito modular absorbe la carga mediante encolamiento/procesamiento eficiente de transacciones de pedido e inventario.

Medida: Disponibilidad del 99.9% durante el pico de campaña y tiempo de respuesta para crear pedidos inferior a 2 segundos.