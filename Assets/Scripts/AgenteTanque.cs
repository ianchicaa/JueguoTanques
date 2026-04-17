using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;

public class AgenteTanque : Agent
{
    [Header("Entrenamiento")]
    [Tooltip("Márcalo para que no dispare y solo aprenda a perseguir")]
    public bool modoEntrenamiento = true; // <-- NUESTRO SEGURO DE ARMAS

    [Header("Ajustes de Movimiento")]
    public float velocidadMovimiento = 5f;
    public float velocidadGiro = 150f;
    public Transform objetivo; 

    [Header("Armas del Terminator")]
    public GameObject prefabBalaEnemiga;
    public Transform puntoDisparo;
    public float distanciaParaDisparar = 8f; 
    public float tiempoEntreDisparos = 1.2f;
    private float temporizadorDisparo;

    private Rigidbody2D rb;

    void Start()
    {
        if (GestorRed.Instancia != null && !GestorRed.Instancia.soyElHost)
        {
            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
        }
    }

    public override void Initialize()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public override void OnEpisodeBegin()
    {
        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;
        
        transform.localPosition = ObtenerPosicionSegura();
        
        if (objetivo != null)
        {
            objetivo.localPosition = ObtenerPosicionSegura();
        }
    }

    private Vector3 ObtenerPosicionSegura()
    {
        Vector3 posicionIntento = Vector3.zero;
        bool posicionValida = false;
        int intentosMaximos = 50; 
        int intentos = 0;
        float radioDelTanque = 0.6f; 

        while (!posicionValida && intentos < intentosMaximos)
        {
            posicionIntento = new Vector3(Random.Range(-6f, 6f), Random.Range(-3f, 3f), 0f);
            Collider2D[] cosasTocadas = Physics2D.OverlapCircleAll(posicionIntento, radioDelTanque);
            
            bool chocaConAlgo = false;
            foreach(Collider2D cosa in cosasTocadas)
            {
                if (cosa.CompareTag("Muro") || cosa.CompareTag("Jugador") || cosa.CompareTag("Enemigo"))
                {
                    chocaConAlgo = true;
                    break;
                }
            }

            if (!chocaConAlgo) posicionValida = true;
            intentos++;
        }

        return posicionIntento;
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(transform.localPosition.x);
        sensor.AddObservation(transform.localPosition.y);
        sensor.AddObservation(transform.localRotation.z);
        if (objetivo != null) {
            sensor.AddObservation(objetivo.localPosition.x);
            sensor.AddObservation(objetivo.localPosition.y);
        } else {
            sensor.AddObservation(0f);
            sensor.AddObservation(0f);
        }
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        if (objetivo == null || (GestorRed.Instancia != null && !GestorRed.Instancia.soyElHost)) return;

        float movimientoY = actions.ContinuousActions[0]; 
        float rotacionX = actions.ContinuousActions[1];   

        rb.linearVelocity = transform.up * movimientoY * velocidadMovimiento;
        rb.angularVelocity = -rotacionX * velocidadGiro;

        AddReward(-0.001f);
    }

    void Update()
    {
        if (GestorRed.Instancia != null && !GestorRed.Instancia.soyElHost) return;

        bool haDisparadoIA = false;
        temporizadorDisparo -= Time.deltaTime;

        if (objetivo != null)
        {
            float distancia = Vector3.Distance(transform.position, objetivo.position);
            Vector3 direccionHaciaObjetivo = (objetivo.position - transform.position).normalized;
            float anguloVision = Vector3.Angle(transform.up, direccionHaciaObjetivo);

            // AQUÍ ESTÁ EL BLOQUEO: Solo dispara si modoEntrenamiento es FALSE
            if (!modoEntrenamiento && distancia <= distanciaParaDisparar && temporizadorDisparo <= 0f && anguloVision < 45f)
            {
                Disparar();
                temporizadorDisparo = tiempoEntreDisparos;
                haDisparadoIA = true;
            }
        }

        if (GestorRed.Instancia != null)
        {
            GestorRed.Instancia.EnviarPosicion("ia", transform.position.x, transform.position.y, transform.eulerAngles.z, 0f, haDisparadoIA);
        }
    }

    void Disparar()
    {
        if (prefabBalaEnemiga != null && puntoDisparo != null)
        {
            Instantiate(prefabBalaEnemiga, puntoDisparo.position, puntoDisparo.rotation);
        }
    }

    private void OnCollisionEnter2D(Collision2D choque)
    {
        if (choque.gameObject.CompareTag("Jugador")) 
        { 
            AddReward(5f); 
            EndEpisode();  
        }
        else if (choque.gameObject.CompareTag("Muro")) 
        { 
            AddReward(-1f); 
        }
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var accionesContinuas = actionsOut.ContinuousActions;
        accionesContinuas[0] = Input.GetAxisRaw("Vertical");
        accionesContinuas[1] = Input.GetAxisRaw("Horizontal");
    }
}