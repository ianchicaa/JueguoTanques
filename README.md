Tanques Multijugador AI

Sistema de combate táctico 2D con arquitectura Multijugador Online e Inteligencia Artificial basada en Redes Neuronales.


Este proyecto representa el ciclo completo de desarrollo de un videojuego moderno, integrando comunicación en tiempo real mediante WebSockets, entrenamiento de agentes inteligentes con ML-Agents y una metodología de desarrollo guiada por especificación (SDD).


🚀 Características del Proyecto

Multijugador Real-Time: Sincronización de posición, rotación y combate entre múltiples clientes a través de un servidor dedicado en Node.js.


IA Enemiga "Terminator V10": Agente entrenado mediante Aprendizaje por Refuerzo (Reinforcement Learning). Utiliza sensores de percepción de rayos (Raycasts) para detectar muros y perseguir al jugador de forma autónoma.


Arquitectura de Red Host/Client: Lógica de autoridad delegada donde el Host gestiona la simulación de la IA y el servidor replica el estado a los clientes de forma eficiente.


Safe Spawn System: Algoritmo de reaparición inteligente que utiliza detección de área circular para evitar que los tanques queden atrapados en obstáculos al iniciar la partida.


Desarrollo Profesional (OpenSpec): Todo el desarrollo está respaldado por una documentación técnica rigurosa que define los objetivos, planes y comportamientos antes de la implementación.


📂 Estructura del Repositorio

Plaintext
.
├── Assets/                 # Código fuente C#, Prefabs y Assets de Unity

├── Packages/               # Gestión de dependencias de Unity

├── ProjectSettings/        # Configuración global del motor

├── specs/                  # Documentación OpenSpec (Metodología SDD)

│   ├── foundations.md      # Objetivos y restricciones del proyecto

│   ├── spec.md             # Comportamiento detallado de la IA

│   └── plan.md             # Estrategia de implementación técnica

├── docs/                   # Traçabilitat y análisis

│   ├── prompts-log.md      # Registro de interacción con IA y resolución de bugs

│   └── analisis_sdd.pdf    # Valoración crítica y resultados finales

├── server.js               # Servidor de WebSockets (Node.js)

├── package.json            # Dependencias del servidor

└── README.md               # Documentación principal

🛠️ Requisitos e Instalación

Servidor (Backend)

El servidor corre sobre Node.js y se encarga de retransmitir los paquetes de datos entre jugadores.


Instalar dependencias: npm install


Ejecutar servidor: node server.js o mediante PM2: pm2 start server.js


Cliente (Unity)

Abrir el proyecto con Unity 6 o superior.


Configurar la IP del servidor en el objeto GestorRed dentro de la escena principal.


Realizar el Build para Windows/Mac o ejecutar directamente desde el editor.


🧠 Entrenamiento de la IA

El agente utiliza el modelo TerminatorV10.onnx. Para reentrenar al agente:


Activar el Modo Entrenamiento en el Inspector del AgenteTanque.


Utilizar el entorno de Python de ML-Agents:


Bash

mlagents-learn config_tanque.yaml --run-id=NombreSesion

Una vez obtenido el archivo .onnx, sustituirlo en el componente Behavior Parameters.


📄 Metodología y Traçabilitat

Este repositorio es una evidencia de Spec-Driven Development. No se trata solo de código funcional, sino de un proceso guiado:


Los archivos en /specs definen el qué y el cómo.


El archivo /docs/prompts-log.md demuestra el proceso de iteración con herramientas de IA, detallando cómo se corrigieron desviaciones técnicas y errores de lógica durante el entrenamiento.


👤 Autor

Desarrollador: [Joel Chica]


Metodología: Desarrollo guiado por especificación con soporte de IA.


Tecnologías: Unity, C#, Node.js, WebSockets, ML-Agents (TensorFlow/PyTorch).


