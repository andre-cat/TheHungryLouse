using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(AudioSource))]
public class BasicMovement : MonoBehaviour
{
    [SerializeField][Tooltip("Set the speed of the louse")] private float moveSpeed;
    [SerializeField][Tooltip("Set the rotation speed of the louse")] private float rotationSpeed;
    [SerializeField][Tooltip("Set how high the louse jumps")] private float jumpForce;

    private Animator louseAnimator;
    private Rigidbody louseRb;
    private Transform louseBody;
    private int numberOfJumps = 0;
    private AudioSource audioSource;
    const float impulseOfTheBody = 0.3f;

    private float inputForwardMovement, inputLateralMovement;

    private void Start()
    {
        louseRb = GetComponent<Rigidbody>();
        louseBody = transform.Find("Body");


        louseAnimator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        inputForwardMovement = Input.GetAxis("Vertical");
        inputLateralMovement = Input.GetAxis("Horizontal");

        Jump();


        if (inputForwardMovement != 0 || inputLateralMovement != 0)
        {
            MovePlayer();
            louseAnimator.SetBool("move", true);
        }
        else
        {
            louseAnimator.SetBool("move", false);
        }
    }

    private void MovePlayer()
    {
        Vector3 verticalMove = Vector3.up * louseRb.velocity.y;
        Vector3 localDirection = transform.forward;
        Vector3 localMovement = localDirection * inputForwardMovement * moveSpeed;
        louseRb.velocity = localMovement + verticalMove;
        float rotation = inputLateralMovement * rotationSpeed * Time.deltaTime;
        transform.Rotate(Vector3.up * rotation, Space.World);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            numberOfJumps = 0;
        }
    }

    private void Jump()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            if (numberOfJumps < 2)
            {
                numberOfJumps += 1;
                louseBody.position += Vector3.up * impulseOfTheBody;
                louseRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                audioSource.Play();
                louseBody.position -= Vector3.up * impulseOfTheBody;
            }
        }
    }

}
