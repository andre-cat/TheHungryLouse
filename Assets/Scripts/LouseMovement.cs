using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(AudioSource))]
public class LouseMovement : MonoBehaviour
{
    [Header("Louse movement settings")]
    [SerializeField]
    [Tooltip("Set the speed of the louse")]
    private float moveSpeed;
    [SerializeField]
    [Tooltip("Set the rotation speed of the louse")]
    private float rotationSpeed;
    [SerializeField]
    [Tooltip("Set how high the louse jumps")]
    private float jumpForce;
	[Header("Final Ui Panel")]
	[SerializeField]
	public GameObject uiFinalPanel;

    private Quaternion saveRotation;    
    private Animator louseAnimator;
    private ParticleSystem louseParticles;
    private float inputForwardMovement, inputLateralMovement, savedSpeed;
    private Rigidbody louseRb;
    private Collider louseCollider;
    private Transform louseBody;
    private int numberOfJumps = 0;
    private Vector3 restartPosition;
    private AudioSource audioSource;

    private void Start()
    {
        louseRb = GetComponent<Rigidbody>();
        louseCollider = GetComponent<Collider>();
        louseBody = transform.Find("Body");

        restartPosition = transform.position;

        louseParticles = transform.Find("Particle System").GetComponent<ParticleSystem>();
        louseAnimator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        savedSpeed = moveSpeed;
    }
    private void Update()
    {
        const int verticalLimit = 15;
        inputForwardMovement = Input.GetAxis("Vertical");
        inputLateralMovement = Input.GetAxis("Horizontal");

        if (numberOfJumps < 2) Jump();
        if (transform.position.y <= verticalLimit) RestoreToTheLastPosition();
        if (inputForwardMovement != 0 || inputLateralMovement != 0)
        {
            MovePlayer();
            louseAnimator.enabled = true;
        }
        else
        {
            louseAnimator.enabled = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Child"))
        {
        	if (IsOnAChild())
            {
                transform.parent = collision.transform;
            	numberOfJumps = 0;
            	return;
            }
        	moveSpeed = 0;	
        }
		numberOfJumps = 0;
    }

    private void OnCollisionExit()
    {
        transform.SetParent(null);
        moveSpeed = savedSpeed;
    }

    private void OnTriggerEnter(Collider other)
    {
        // Checkpoit interaction
        Vector3 checkpointPosition = other.gameObject.transform.position;
        if (other.gameObject.CompareTag("Checkpoint"))
        {
            restartPosition = checkpointPosition;
            Destroy(other.gameObject);
        }
		if (other.gameObject.CompareTag("Finish"))
        {
            restartPosition = checkpointPosition;
            uiFinalPanel.SetActive(true);
        }
    }

    private void MovePlayer()
    {
        // If The player is in the air, it keeps the vertical movement   
        Vector3 verticalMove = Vector3.up * louseRb.velocity.y;

        // Used for focus in the local forward although the louse rotate 
        Vector3 localDirection = transform.forward;
        Vector3 localMovement = localDirection * inputForwardMovement * moveSpeed;
        louseRb.velocity = localMovement + verticalMove;

        float rotation = inputLateralMovement * rotationSpeed * Time.deltaTime;
        transform.Rotate(Vector3.up * rotation, Space.World);
    }

    private void Jump()
    {
        // Distance in which the body moves to simulate momentum 
        const float impulseOfTheBody = 0.3f;

        //Only take impulse in the first jump
        if (Input.GetKeyDown(KeyCode.Space) && numberOfJumps == 0)
        {
            louseBody.position -= (Vector3.up * impulseOfTheBody);
            louseRb.angularVelocity = Vector3.zero;
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            // Only return the body position in the first jump
            if (numberOfJumps < 1)
            {
                louseBody.position += (Vector3.up * impulseOfTheBody);
            }
            // Overrides the current vertical speed before applying the jump.
            louseRb.angularVelocity = Vector3.zero;
            louseRb.velocity = new Vector3(louseRb.velocity.x, 0, louseRb.velocity.z);
            louseRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            numberOfJumps += 1;
            // Jumping sound
            audioSource.Play();
        }
    }

    // Detect that if the louse is on the head of a child
    private bool IsOnAChild()
    {
        const float rayLength = 1f;
        // Collider's borders
        float leftBorder = transform.position.x - GetComponent<Collider>().bounds.size.x / 2;
        float rightBorder = transform.position.x + GetComponent<Collider>().bounds.size.x / 2;
        float frontBorder = transform.position.z - GetComponent<Collider>().bounds.size.z / 2;
        float backBorder = transform.position.z + GetComponent<Collider>().bounds.size.z / 2;
        
        // Different positions in which Raycasts will be fired
        Vector3[] initialPositionsRaycasts = new Vector3[]{
            new Vector3(transform.position.x, transform.position.y, frontBorder), // Front
            new Vector3(rightBorder, transform.position.y, frontBorder), // Front-Right
            new Vector3(rightBorder, transform.position.y, transform.position.z), // Right
            new Vector3(rightBorder, transform.position.y, backBorder), // Back-Right
            new Vector3(transform.position.x, transform.position.y, backBorder), // Back
            new Vector3(leftBorder, transform.position.y, backBorder), // Back-Left
            new Vector3(leftBorder, transform.position.y, transform.position.z), // Left
            new Vector3(leftBorder, transform.position.y, frontBorder)}; // Front-Left

        for (int i = 0; i < initialPositionsRaycasts.Length; i++)
        {
            // Fire the Raycast
            RaycastHit hit;
            if (Physics.Raycast(initialPositionsRaycasts[i], Vector3.down, out hit, rayLength))
                return true;
        }
        return false;
    }

    public void RestoreToTheLastPosition()
    {
        moveSpeed = savedSpeed;
        transform.position = restartPosition;
        transform.Rotate(0, -transform.rotation.eulerAngles.y, 0);
        louseParticles.Play();
    }

}