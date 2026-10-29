Supuestos y decisiones iniciales
-¿Qué asumo? 
Conectividad y dispositivos: Se asume que los repartidores y operadores de tienda cuentan con dispositivos móviles con acceso a internet móvil funcional para reportar estados en tiempo real.

Infraestructura de terceros: Se asume que los servicios externos (como la pasarela de pagos, el servicio de geolocalización y las APIs de notificación) mantendrán una alta disponibilidad y estabilidad operativa.

Procesos de almacén: Se asume que cada almacén distribuido cuenta con personal capacitado para registrar la salida física de la mercancía mediante códigos de barras o escaneo digital básico.

-¿Qué decisiones ya tomé?

Centralización del núcleo: Se decidió implementar una plataforma centralizada que unifique la recepción de pedidos, la validación de inventario y la asignación de rutas de última milla, eliminando la operación fragmentada actual.

Arquitectura de integración: Se acordó que el sistema no gestionará directamente las pasarelas de pago ni la cartografía GPS desde cero, sino que se integrará mediante servicios externos especializados.

Alcance transaccional inicial: Se decidió acotar el alcance de esta primera fase a la gestión logística y operativa, excluyendo temporalmente algoritmos predictivos complejos de demanda basados en inteligencia artificial.

-¿Qué aún no sé? 

Volumetría exacta y concurrencia: Todavía no se conoce con precisión el volumen pico de pedidos simultáneos que la plataforma deberá soportar durante temporadas de alta demanda (como días festivos o campañas de descuentos masivos)

Restricciones de infraestructura heredada: Aún no se tiene el detalle técnico completo de las limitaciones del ERP/CRM interno actual para realizar sincronizaciones en tiempo real sin generar bloqueos.  

Capacidad de las flotas de terceros: No se ha definido el porcentaje exacto de entregas que se realizarán con flotas propias frente a operadores logísticos externos.

¿Qué habrá que confirmar luego? 
 
de APIs del ERP: Será necesario realizar pruebas técnicas con el equipo de sistemas para confirmar los métodos y protocolos de integración con el ERP/CRM interno. 

Revisión de SLAs con proveedores externos: Se deberán confirmar los costos, límites de peticiones por segundo y acuerdos de nivel de servicio (SLA) de la geolocalización y la pasarela de pagos.

Pruebas de campo con operadores: Se requerirá validar los flujos de la interfaz móvil directamente con repartidores y administradores de almacén en un entorno controlado antes del despliegue final.
