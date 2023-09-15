using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class LouseMovement : MonoBehaviour
{
    [Header("Louse movement settings")] 
    [SerializeField] [Tooltip("Set the speed of the louse")] 
    private float moveSpeed;
    [SerializeField] [Tooltip("Set the rotation speed of the louse")] 
    private float rotationSpeed;
    [SerializeField] [Tooltip("Set how high the louse jumps")]
    private float jumpForce;
    [Header("Jump sound settings")]
    [SerializeField] [Tooltip("Is the resource with audio data")]
    private AudioClip audioFile;
    [SerializeField] [Tooltip("Is the component that is attached to the GameObject")]
    private AudioSource audioSource;
    private Animator louseAnimator;
    private float inputForwardMovement, inputLateralMovement;
    private Rigidbody louseRb;
    private Transform louseBody;
    private int numberOfJumps = 0;
    private Vector3 restartPosition;
    
    private void Start()
    {
        louseRb = GetComponent<Rigidbody>();
        louseBody = transform.Find("Body");

        restartPosition = transform.position;
        
        louseAnimator = GetComponent<Animator>();
        audioSource.clip = audioFile;
    }
    private void Update()
    {
        const int verticalLimit = 15;
        inputForwardMovement = Input.GetAxis("Vertical");
        inputLateralMovement = Input.GetAxis("Horizontal");
        
        if (numberOfJumps < 2) Jump();
        if (IsOnAChild()) numberOfJumps = 0;
        if (transform.position.y <= verticalLimit) RestoreToTheLastPosition();
        if (inputForwardMovement != 0 || inputLateralMovement != 0)
        {
            MovePlayer();
            louseAnimator.enabled = true;
        }
        else
        {
            //louseRb.angularVelocity = Vector3.zero;
            louseAnimator.enabled = false;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        Animator anim = other.gameObject.GetComponent<Animator>();
        Vector3 checkpointPosition = other.gameObject.transform.position;
        if (other.gameObject.CompareTag("Checkpoint"))
        {
            restartPosition = checkpointPosition;
            Destroy(other.gameObject);
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
        if (Input.GetKeyDown(KeyCode.Space) && numberOfJumps < 1)
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
        const float rayLength = 1.2f;
        if (Physics.Raycast(transform.position, Vector3.down, out var hit, rayLength))
        {
            Vector3 normal = hit.normal;
            if (normal.y > 0.5f) return true;
        }
        return false;
    }
    
    private void RestoreToTheLastPosition()
    {
        transform.position = restartPosition;
    }
}
