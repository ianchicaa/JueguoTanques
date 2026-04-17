# Registro de Prompts e Iteraciones con IA (Prompts-Log)

## Iteración 1: La Lobotomía Multijugador (Sincronizar IA por Red)
- **Mi Prompt:** *Le adjunté el script AgenteTanque.cs, GestorRed.cs y server.js y le pedí que el tanque IA (gris) no diera tirones ni se desdoblara en el multijugador.*
- **Respuesta de la IA:** Me generó un nuevo `AgenteTanque.cs` que en la función `Start()` comprobaba si no éramos el Host. Si era el caso, desactivaba las físicas y lo convertía en una "marioneta" manejada por el `GestorRed`.
- **Problema detectado:** Al ejecutar el código en Unity 6, saltó un warning amarillo: `Rigidbody2D.isKinematic is obsolete`.
- **Corrección en Prompt:** Le envié una captura del error.
- **Ajuste de la IA:** Cambió `isKinematic = true` por la sintaxis moderna de Unity 6: `GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;`.

## Iteración 2: El Bug del Atasco y los Muros (Refinando la IA)
- **Mi Prompt:** *"ahora quiero mejorar la ia, y que me persiga de verdad, ahora el tanque de la ia es un poco tonto y va a su rollo, vamos a mejorarlo. Si no te acuerdas de ficheros... preguntame."*
- **Análisis de la IA:** La IA pidió ver capturas de mi Inspector. Tras pasárselas, detectó que el `Ray Perception Sensor 2D` estaba puesto, pero la lógica del script recompensaba acercarse y castigaba alejarse (causando que el tanque se quedara atascado contra los muros intentando no alejarse).
- **Corrección aplicada:** La IA generó un nuevo `AgenteTanque.cs` eliminando las variables de `distanciaAnterior` y cambiando los premios en `OnCollisionEnter2D`.

## Iteración 3: Reaparición Bugueada (Safe Spawn)
- **Mi Prompt:** *"antes de hacer esto quiero solucionar otra cosa, y es que a veces cuando reaparece el tanque, el tanque reaparece encima del muro y se bugea."*
- **Solución Generada:** La IA propuso la función `ObtenerPosicionSegura()` usando `Physics2D.OverlapCircleAll`. Sustituyó la lógica del `OnEpisodeBegin()`. Esto demostró que la IA es capaz de prever errores físicos antes del entrenamiento neuronal.

## Iteración 4: El Seguro de Armas (Entrenamiento Limpio)
- **Mi Prompt:** *"he tenido que parar el entrenamiento porque la ia ha matado al tanque rojo con las balas, hay que quitarle eso y despues volver a hacer el entrenamiento."*
- **Solución Generada:** La IA añadió un booleano llamado `modoEntrenamiento`. Bloqueó la instanciación de prefabs de balas si la variable era true, permitiendo entrenar de forma ininterrumpida el movimiento. Tras esto, se generó y aplicó el cerebro `.onnx` final (TerminatorV10).