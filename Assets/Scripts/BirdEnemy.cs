using UnityEngine;
using UnityEngine.SceneManagement; // Necesario para reiniciar la escena

public class BirdEnemy : MonoBehaviour
{
    public Transform playerTransform;    // Transform del personaje principal
    public float orbitDistance = 40f;    // Distancia a la que el pájaro orbita al personaje
    public float orbitSpeed = 20f;       // Velocidad de rotación del pájaro alrededor del personaje
    public float approachSpeed = 10f;    // Velocidad de acercamiento inicial
    public float initialDistance = 40f;  // Distancia inicial antes de acercarse
    public float stationaryThreshold = 1f; // Umbral de velocidad para considerar que el personaje está quieto

    private bool isOrbiting = false;     // Indica si el pájaro está orbitando al personaje
    private Vector3 initialPosition;     // Posición inicial del pájaro
    private Vector3 lastPlayerPosition;  // Última posición conocida del personaje principal
    private Rigidbody playerRigidbody;   // Rigidbody del personaje principal para calcular su velocidad

    private float startAngle; // Ángulo inicial de la órbita
    private float endAngle;   // Ángulo final de la órbita
    private float currentAngle; // Ángulo actual de la órbita

    void Start()
    {
        initialPosition = transform.position;
        lastPlayerPosition = playerTransform.position;
        playerRigidbody = playerTransform.GetComponent<Rigidbody>();
        startAngle = Vector3.SignedAngle(Vector3.forward, playerTransform.position - transform.position, Vector3.up) + 200f;
        endAngle = startAngle + 150f; // Órbita de 180 grados
        currentAngle = startAngle;
    }

    void Update()
    {
        // Calcula la dirección hacia el personaje principal
        Vector3 directionToPlayer = playerTransform.position - transform.position;

        // Calcula la distancia al personaje
        float distanceToPlayer = directionToPlayer.magnitude;

        // Calcula la velocidad actual del personaje principal
        Vector3 playerVelocity = (playerTransform.position - lastPlayerPosition) / Time.deltaTime;
        lastPlayerPosition = playerTransform.position;
        
        if (!isOrbiting)
        {
            // Si el enemigo está dentro de la distancia inicial, comienza a acercarse
            if (distanceToPlayer <= initialDistance)
            {
                isOrbiting = true;
            }
            else
            {
                // Mueve al enemigo desde su posición inicial hacia el personaje principal
                //Vector3 targetPosition = playerTransform.position;
                //transform.position = Vector3.MoveTowards(transform.position, targetPosition, approachSpeed * Time.deltaTime);
                isOrbiting = true;
            }
        }
        else
        {
            // Si la velocidad del personaje es menor que el umbral, el enemigo se acerca
            if (playerVelocity.magnitude < stationaryThreshold)
            {
                Vector3 targetPosition = playerTransform.position;
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, approachSpeed * Time.deltaTime);
            }
            else
            {
                // Orbita alrededor del personaje
                OrbitAroundPlayer();
            }
        }
    }

    void OrbitAroundPlayer()
    {
        // Calcula la nueva posición de órbita
        currentAngle += orbitSpeed * Time.deltaTime;

        if (currentAngle > endAngle)
        {
            //currentAngle -= orbitSpeed * Time.deltaTime; // Reinicia la órbita
            orbitSpeed = orbitSpeed * -1;
        }
        if (currentAngle < startAngle)
        {
            orbitSpeed = orbitSpeed * -1;

        }

        float radians = Mathf.Deg2Rad * currentAngle;
        Vector3 offset = new Vector3(Mathf.Sin(radians), 0, Mathf.Cos(radians)) * orbitDistance;
        Vector3 desiredPosition = playerTransform.position + offset;

        // Mantiene la altura del enemigo igual a la altura del personaje principal
        desiredPosition.y = playerTransform.position.y;
        
        // Aplica la nueva posición
        transform.position = desiredPosition;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Si colisiona con el jugador, reinicia 
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            LouseMovement louse = collision.gameObject.GetComponent<LouseMovement>();
            louse.RestoreToTheLastPosition();
        }
    }
}
