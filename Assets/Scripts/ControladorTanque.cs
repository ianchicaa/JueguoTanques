using UnityEngine;

public class ControladorTanque : MonoBehaviour
{
    [Header("Ajustes de Movimiento")]
    public float velocidadMovimiento = 5f;
    public float velocidadGiro = 150f;

    [Header("Referencias Movimiento")]
    public Transform pivoteTorreta; 

    [Header("Referencias Disparo")]
    public GameObject prefabBala; 
    public Transform puntoDisparo; 

    void Start()
    {
        float xAleatorio = Random.Range(-6f, 6f);
        float yAleatorio = Random.Range(-3f, 3f);
        transform.position = new Vector3(xAleatorio, yAleatorio, 0f);
    }

    void Update()
    {
        if (!Application.isFocused) return;

        MoverTanque();
        ApuntarTorreta();

        bool disparo = Input.GetButtonDown("Fire1"); 

        if (GestorRed.Instancia != null)
        {
            // ¡AQUÍ ESTÁ EL CAMBIO! Le añadimos "jugador" al principio
            GestorRed.Instancia.EnviarPosicion(
                "jugador", 
                transform.position.x, 
                transform.position.y, 
                transform.eulerAngles.z,      
                pivoteTorreta.eulerAngles.z,
                disparo 
            );
        }

        if (disparo) Disparar();
    }

    void MoverTanque()
    {
        float movimientoY = Input.GetAxis("Vertical");
        float movimientoX = Input.GetAxis("Horizontal");

        transform.Translate(Vector3.up * movimientoY * velocidadMovimiento * Time.deltaTime);
        transform.Rotate(Vector3.forward * -movimientoX * velocidadGiro * Time.deltaTime);
    }

    void ApuntarTorreta()
    {
        if (pivoteTorreta != null)
        {
            Vector3 posicionRaton = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            posicionRaton.z = 0f; 

            Vector3 direccion = posicionRaton - pivoteTorreta.position;
            float angulo = Mathf.Atan2(direccion.y, direccion.x) * Mathf.Rad2Deg;

            pivoteTorreta.rotation = Quaternion.Euler(0, 0, angulo - 90f);
        }
    }

    void Disparar()
    {
        if (prefabBala != null && puntoDisparo != null)
        {
            Instantiate(prefabBala, puntoDisparo.position, puntoDisparo.rotation);
        }
    }
}