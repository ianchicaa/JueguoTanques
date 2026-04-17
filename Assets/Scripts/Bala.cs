using UnityEngine;

public class Bala : MonoBehaviour
{
    public float velocidad = 15f;
    public float tiempoDeVida = 2f;
    
    [Header("Ajustes de Daño")]
    public bool esBalaEnemiga = false; // Para que no nos matemos a nosotros mismos
    public GameObject prefabExplosion; 

    void Start()
    {
        Destroy(gameObject, tiempoDeVida);
    }

    void Update()
    {
        transform.Translate(Vector3.up * velocidad * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D choque)
    {
        if (choque.CompareTag("Muro")) 
        {
            Destroy(gameObject);
        }
        // Si soy MI bala y choco contra el ENEMIGO...
        else if (!esBalaEnemiga && choque.CompareTag("Enemigo"))
        {
            Explotar(choque.gameObject);
        }
        // Si soy la bala del ENEMIGO y choco contra MÍ...
        else if (esBalaEnemiga && choque.CompareTag("Jugador"))
        {
            Explotar(choque.gameObject);
        }
    }

    void Explotar(GameObject objetivo)
    {
        if (prefabExplosion != null)
        {
            Instantiate(prefabExplosion, objetivo.transform.position, Quaternion.identity);
        }
        Destroy(objetivo);   // Destruimos el tanque
        Destroy(gameObject); // Destruimos la bala
    }
}