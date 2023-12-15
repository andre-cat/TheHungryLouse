using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class BirdEnemy : MonoBehaviour
{
    public Transform playerTransform;    // Transform del personaje principal
    public float orbitDistance = 40f;    // Distancia a la que el p�jaro orbita al personaje
    public float orbitSpeed = 20f;       // Velocidad de rotaci�n del p�jaro alrededor del personaje
    public float approachSpeed = 10f;    // Velocidad de acercamiento inicial
    public float initialDistance = 40f;  // Distancia inicial antes de acercarse
    public float stationaryThreshold = 1f; // Umbral de velocidad para considerar que el personaje est� quieto

    private bool isOrbiting = false;     // Indica si el p�jaro est� orbitando al personaje
    private Vector3 initialPosition;     // Posici�n inicial del p�jaro
    private Vector3 lastPlayerPosition;  // �ltima posici�n conocida del personaje principal
    private Rigidbody playerRigidbody;   // Rigidbody del personaje principal para calcular su velocidad

    private float startAngle; // �ngulo inicial de la �rbita
    private float endAngle;   // �ngulo final de la �rbita
    private float currentAngle; // �ngulo actual de la �rbita

    [SerializeField] private AudioSource punchAudioSource;

    void Start()
    {
        initialPosition = transform.position;
        lastPlayerPosition = playerTransform.position;
        playerRigidbody = playerTransform.GetComponent<Rigidbody>();
        startAngle = Vector3.SignedAngle(Vector3.forward, playerTransform.position - transform.position, Vector3.up) + 200f;
        endAngle = startAngle + 150f; // �rbita de 180 grados
        currentAngle = startAngle;
    }

    void Update()
    {
        // Calcula la direcci�n hacia el personaje principal
        Vector3 directionToPlayer = playerTransform.position - transform.position;

        // Calcula la distancia al personaje
        float distanceToPlayer = directionToPlayer.magnitude;

        // Calcula la velocidad actual del personaje principal
        Vector3 playerVelocity = (playerTransform.position - lastPlayerPosition) / Time.deltaTime;
        lastPlayerPosition = playerTransform.position;

        if (!isOrbiting)
        {
            // Si el enemigo est� dentro de la distancia inicial, comienza a acercarse
            if (distanceToPlayer <= initialDistance)
            {
                isOrbiting = true;
            }
            else
            {
                // Mueve al enemigo desde su posici�n inicial hacia el personaje principal
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
        // Calcula la nueva posici�n de �rbita
        currentAngle += orbitSpeed * Time.deltaTime;

        if (currentAngle > endAngle)
        {
            //currentAngle -= orbitSpeed * Time.deltaTime; // Reinicia la �rbita
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

        // Aplica la nueva posici�n
        transform.position = desiredPosition;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            punchAudioSource.Play();
            LouseMovement louse = collision.gameObject.GetComponent<LouseMovement>();
            louse.RestoreToTheLastPosition();
        }
    }
}
