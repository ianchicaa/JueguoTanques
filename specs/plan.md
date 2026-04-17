# Plan de Implementación Estratégica

## Fase 1: Lobotomía Multijugador
- Modificar el script `AgenteTanque.cs` para apagar el `Rigidbody2D` (ponerlo en Kinematic) si el jugador no es el Host de la partida.
- Redirigir el movimiento de la IA a través del `GestorRed.cs` si se juega como cliente.

## Fase 2: Lógica de Castigos y Recompensas (Fix del Bug de atasco)
- Eliminar la lógica basada puramente en distancia (que provocaba atascos en los muros).
- Implementar castigos por choque en `OnCollisionEnter2D` pero sin `EndEpisode` contra los muros.

## Fase 3: Safe Spawn
- Escribir la función `ObtenerPosicionSegura()` utilizando un bucle while y detectores de área circular para evitar reapariciones defectuosas en el entrenamiento.

## Fase 4: Entrenamiento Puro
- Bloquear el código de disparo.
- Limpiar el "cerebro" antiguo en el Inspector.
- Lanzar el entrenamiento por consola (Python).
- Sustituir el modelo resultante (`.onnx`) en Unity y reactivar las armas.