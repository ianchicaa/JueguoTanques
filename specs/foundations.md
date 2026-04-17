# Foundations: IA del Tanque Enemigo

## 1. Contexto
El proyecto es un juego arcade 2D multijugador online de tanques ("TanquesMultijugador"). El jugador controla un tanque rojo. Existe un tanque gris controlado por Inteligencia Artificial entrenada con Unity ML-Agents y Python.

## 2. Objetivos (Goals)
- Desarrollar un Agente Tanque (IA) que persiga activamente al jugador.
- La IA debe ser capaz de esquivar muros (obstáculos) y no quedarse atascada intentando atravesarlos.
- La IA debe tener una condición de disparo cuando tenga línea de visión directa con el jugador.

## 3. Restricciones (Constraints)
- La IA debe funcionar en armonía con el sistema Multijugador (el Host calcula la IA, el Cliente solo recibe coordenadas).
- El tanque de la IA no puede reaparecer (spawnear) dentro de colisionadores (muros/cajas) al inicio del episodio o al reiniciar la partida.
- El modelo de ML-Agents debe separar el entrenamiento de movimiento del entrenamiento de disparo para evitar conflictos tempranos.