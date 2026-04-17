using UnityEngine;
using NativeWebSocket;
using UnityEngine.SceneManagement;

[System.Serializable]
public class DatosMovimiento
{
    public string tipo;
    public float x;
    public float y;
    public float rotacionCuerpoZ;
    public float rotacionTorretaZ;
    public bool haDisparado;
}

public class GestorRed : MonoBehaviour
{
    public static GestorRed Instancia; 
    WebSocket websocket;

    [Header("Configuración Multijugador")]
    public bool soyElHost = true; 

    [Header("Referencias del Enemigo (Tanque Azul)")]
    public Transform tanqueEnemigo;
    public Transform pivoteTorretaEnemigo;
    public GameObject prefabBalaEnemiga;  
    public Transform puntoDisparoEnemigo; 

    [Header("Referencias de la IA (Tanque Gris)")]
    public Transform tanqueIA;
    public GameObject prefabBalaIA;
    public Transform puntoDisparoIA;

    void Awake() { Instancia = this; }

    async void Start()
    {
        websocket = new WebSocket("ws://204.168.215.154:8080");
        websocket.OnOpen += () => { Debug.Log("🟢 Conectado al Servidor."); };
        websocket.OnMessage += (bytes) =>
        {
            string mensaje = System.Text.Encoding.UTF8.GetString(bytes);
            DatosMovimiento datos = JsonUtility.FromJson<DatosMovimiento>(mensaje);
            
            if (datos.tipo == "reiniciar")
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                return;
            }

            // APLICAMOS LOS DATOS AL INSTANTE (Sin Lerp, sin retrasos)
            if (datos.tipo == "jugador" && tanqueEnemigo != null)
            {
                // Movimiento directo
                tanqueEnemigo.position = new Vector3(datos.x, datos.y, 0f);
                tanqueEnemigo.rotation = Quaternion.Euler(0, 0, datos.rotacionCuerpoZ);
                
                if (pivoteTorretaEnemigo != null)
                {
                    pivoteTorretaEnemigo.rotation = Quaternion.Euler(0, 0, datos.rotacionTorretaZ);
                }

                // Disparo
                if (datos.haDisparado && prefabBalaEnemiga != null && puntoDisparoEnemigo != null)
                {
                    Instantiate(prefabBalaEnemiga, puntoDisparoEnemigo.position, puntoDisparoEnemigo.rotation);
                }
            }
            else if (datos.tipo == "ia" && !soyElHost && tanqueIA != null)
            {
                // Movimiento directo de la IA para el Cliente
                tanqueIA.position = new Vector3(datos.x, datos.y, 0f);
                tanqueIA.rotation = Quaternion.Euler(0, 0, datos.rotacionCuerpoZ);

                if (datos.haDisparado && prefabBalaIA != null && puntoDisparoIA != null)
                {
                    Instantiate(prefabBalaIA, puntoDisparoIA.position, puntoDisparoIA.rotation);
                }
            }
        };
        await websocket.Connect();
    }

    void Update()
    {
        #if !UNITY_WEBGL || UNITY_EDITOR
        if (websocket != null) websocket.DispatchMessageQueue();
        #endif
    }

    public async void EnviarPosicion(string tipoTanque, float posX, float posY, float rotCuerpo, float rotTorreta, bool disparo)
    {
        if (websocket != null && websocket.State == WebSocketState.Open)
        {
            DatosMovimiento misDatos = new DatosMovimiento();
            misDatos.tipo = tipoTanque;
            misDatos.x = posX;
            misDatos.y = posY;
            misDatos.rotacionCuerpoZ = rotCuerpo;
            misDatos.rotacionTorretaZ = rotTorreta;
            misDatos.haDisparado = disparo;

            await websocket.SendText(JsonUtility.ToJson(misDatos));
        }
    }

    public async void EnviarReinicio()
    {
        if (websocket != null && websocket.State == WebSocketState.Open)
        {
            DatosMovimiento datos = new DatosMovimiento();
            datos.tipo = "reiniciar";
            await websocket.SendText(JsonUtility.ToJson(datos));
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private async void OnApplicationQuit() { if (websocket != null) await websocket.Close(); }
}