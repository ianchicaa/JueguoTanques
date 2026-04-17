using UnityEngine;
using UnityEngine.SceneManagement;

public class GestorJuego : MonoBehaviour
{
    [Header("Los Combatientes")]
    public GameObject tanqueJugador;      // El rojo
    public GameObject tanqueIA;           // El gris
    public GameObject tanqueMultijugador; // El azul (TanqueEnemigo)

    [Header("Interfaz")]
    public GameObject botonReinicio;

    private bool juegoTerminado = false;

    void Start()
    {
        // Nos aseguramos de que el botón empiece oculto
        if (botonReinicio != null)
        {
            botonReinicio.SetActive(false);
        }
    }

    void Update()
    {
        // Si el juego ya terminó, el árbitro deja de mirar
        if (juegoTerminado) return;

        // El árbitro cuenta los tanques vivos en este frame
        int tanquesVivos = 0;
        
        if (tanqueJugador != null) tanquesVivos++;
        if (tanqueIA != null) tanquesVivos++;
        if (tanqueMultijugador != null) tanquesVivos++;

        // Si queda 1 solo tanque (o 0), ¡Fin de la partida!
        if (tanquesVivos <= 1)
        {
            TerminarPartida();
        }
    }

    void TerminarPartida()
    {
        juegoTerminado = true;
        
        // Encendemos el botón gigante
        if (botonReinicio != null)
        {
            botonReinicio.SetActive(true);
        }
    }

    public void ReiniciarJuego()
    {
        // Si hay red, avisamos a todo el mundo de que recargue el nivel
        if (GestorRed.Instancia != null)
        {
            GestorRed.Instancia.EnviarReinicio();
        }
        else 
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}