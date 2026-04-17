# Specification: Comportamiento del Agente Tanque

## 1. Recompensas (Rewards)
- **Tocar al Jugador:** Premio masivo (+5f) y finaliza el episodio (EndEpisode) para evitar el farmeo infinito de puntos.
- **Tocar un Muro:** Castigo moderado (-1f), pero el episodio CONTINÚA para enseñar a la IA a maniobrar y salir del choque.
- **Penalización por tiempo:** Castigo minúsculo constante (-0.001f) por cada step para incentivar la rapidez en la caza.

## 2. Sensores (Perception)
- **VectorSensor:** Observa su propia posición (X, Y), su rotación (Z) y la posición del objetivo (X, Y).
- **Ray Perception Sensor 2D:** Utiliza rayos (láseres) configurados a 180 grados para detectar dos etiquetas (Tags) específicas: `Muro` y `Jugador`.

## 3. Comportamientos Específicos
- **Safe Spawn (Reaparición Segura):** Al iniciar un episodio, la IA debe usar un Physics2D.OverlapCircleAll para comprobar que la coordenada aleatoria de reaparición está libre de muros y jugadores. Si está ocupada, debe generar otra coordenada (hasta un máximo de 50 intentos).
- **Modo Entrenamiento (Seguro de armas):** Un booleano `modoEntrenamiento` en el Inspector que bloquee la lógica de disparo (instanciación de balas) mientras la red neuronal aprende exclusivamente el movimiento.