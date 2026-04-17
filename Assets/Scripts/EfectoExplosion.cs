using UnityEngine;

public class EfectoExplosion : MonoBehaviour
{
    public Sprite[] frames; // Aquí guardaremos tus 5 dibujos de la explosión
    public float tiempoPorFrame = 0.05f; // Lo rápido que explota

    private SpriteRenderer renderizador;
    private int frameActual = 0;
    private float temporizador = 0f;

    void Start()
    {
        renderizador = GetComponent<SpriteRenderer>();
        if (frames.Length > 0) renderizador.sprite = frames[0];
    }

    void Update()
    {
        temporizador += Time.deltaTime;
        if (temporizador >= tiempoPorFrame)
        {
            temporizador = 0f;
            frameActual++;

            if (frameActual < frames.Length) {
                renderizador.sprite = frames[frameActual]; // Cambia al siguiente dibujo
            } else {
                Destroy(gameObject); // Cuando acaba la animación, se borra
            }
        }
    }
}