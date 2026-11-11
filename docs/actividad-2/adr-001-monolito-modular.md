# ADR-001: Adopción de una Arquitectura de Monolito Modular para la Plataforma Logística LogiFast

## Estado
Aceptado

## Contexto
La plataforma de retail digital **LogiFast** requiere centralizar la recepción de pedidos multicanal, gestionar inventarios distribuidos en múltiples ubicaciones, coordinar la asignación de rutas de última milla y ofrecer rastreo en tiempo real a los clientes y al equipo de soporte. 

Nos encontramos en una etapa inicial de desarrollo donde el equipo técnico busca un equilibrio entre la velocidad de salida al mercado (time-to-market), la simplicidad operativa y la mantenibilidad a mediano plazo, evitando sobreingeniería innecesaria.

## Opciones Consideradas

### Opción A: Monolito modular con límites claros
* **Descripción:** Una estructura de software unificada a nivel de despliegue, pero fuertemente segregada internamente en dominios o módulos independientes (ej. Pedidos, Inventario, Envíos, Soporte) mediante separación lógica de carpetas y capas.
* **Ventajas:** 
  * Más rápido de construir y desplegar para una operación con crecimiento moderado.
  * Reduce drásticamente la complejidad operativa inicial al no requerir orquestación de red ni infraestructura distribuida.
  * Facilita las transacciones y la consistencia de datos, mejorando la trazabilidad inicial.
* **Desventajas:**
  * Si los desarrolladores no respetan estrictamente los límites internos de los módulos, la base de código puede tender al acoplamiento con el tiempo.

### Opción B: Arquitectura distribuida por microservicios
* **Descripción:** Descomponer la plataforma en servicios completamente independientes desplegados en red para cada dominio (un microservicio de pedidos, uno de inventario, uno de rutas, etc.).
* **Ventajas:**
  * Permite escalar servicios específicos de manera independiente y separar equipos de trabajo por dominio.
* **Desventajas:**
  * Aumenta de forma exponencial la complejidad operativa (gestión de contenedores, redes, latencia).
  * Exige coordinar consistencia eventual y transacciones distribuidas complejas.
  * Representa un sobrediseño técnico severo para la etapa actual del negocio de LogiFast.

## Decisión
Se decide adoptar la **Opción A (Monolito modular con límites claros)**. Esta arquitectura permite delimitar fuertemente los dominios de negocio (pedidos, inventario, logística) dentro de una misma aplicación base, asegurando una implementación limpia sin introducir la complejidad operativa de los microservicios antes de contar con evidencia empírica de que el crecimiento del volumen de operaciones lo justifique[cite: 7].

## Consecuencias y Trade-offs

### Beneficios (Pros)
* **Menor costo y complejidad de infraestructura:** Se simplifican las pruebas locales, la depuración (debugging) y los despliegues continuos.
* **Simplicidad transaccional:** Garantizar la integridad de los datos entre inventario y pedidos es mucho más directo dentro de una misma base de datos relacional y contexto unificado.
* **Alineación con el negocio:** Permite iterar rápidamente sobre las reglas logísticas de última milla sin perder tiempo configurando infraestructura de red o servicios distribuidos.

### Riesgos y Concesiones (Trade-offs)
* **Deuda técnica potencial por acoplamiento:** Si el equipo ignora los límites lógicos de los módulos, las dependencias cruzadas pueden dificultar la evolución del código. Para mitigar esto, se aplicarán revisiones estrictas de código (Code Reviews) y separación estricta de capas en la solución (por ejemplo, dividiendo en proyectos o capas de API, DataAccess y Domain).
* **Escalabilidad de despliegue:** Ante un crecimiento masivo y sostenido de un solo submódulo (ej. consultas masivas de rastreo), no se podrá escalar ese módulo de forma independiente hasta realizar una futura refactorización para extraerlo como servicio.