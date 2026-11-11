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

3. Escenario de Modificabilidad / Mantenibilidad (Reglas de Negocio)
Estímulo: El equipo de operaciones requiere actualizar las reglas de negocio para el cálculo de tarifas o la asignación de rutas de última milla.

Entorno: Fase de evolución y mantenimiento del software.

Respuesta: El equipo de desarrollo modifica la lógica correspondiente dentro del módulo aislado de rutas/logística sin alterar los esquemas de autenticación o la pasarela de pagos.

Medida (Métrica): El cambio de la regla de negocio debe implementarse, probarse e integrarse sin requerir una refactorización completa ni un despliegue total de la aplicación monolítica, tomando un tiempo menor a 1 jornada de desarrollo.

4. Escenario de Seguridad (Confidencialidad de Datos)
Estímulo: Un actor malintencionado intenta interceptar el tráfico de red o acceder directamente a las tablas de la base de datos que contienen información personal de los clientes (direcciones, teléfonos) y accesos.

Entorno: Tráfico en redes públicas de internet y almacenamiento en servidores.

Respuesta: El sistema bloquea accesos no autorizados mediante políticas estrictas de control y mantiene los datos cifrados.

Medida (Métrica): Toda la información sensible debe permanecer cifrada tanto en tránsito (usando HTTPS/TLS) como en reposo en la base de datos, cumpliendo con los estándares básicos de protección de datos.