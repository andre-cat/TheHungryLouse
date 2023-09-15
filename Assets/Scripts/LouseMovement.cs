using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LouseController_Script : MonoBehaviour
{
    [Header("Louse movement settings")] 
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private float rotationSpeed;
    [SerializeField] 
    private float jumpForce;
    [SerializeField]
    private PawsMovement[] paws;
    private float inputForwardMovement, inputLateralMovement;
    private Rigidbody louseRb;
    private Transform louseBody;
    private int numberOfJumps = 0;
    
    private void Start()
    {
        louseRb = GetComponent<Rigidbody>();
        louseBody = transform.Find("Body");
    }
    private void Update()
    {
        inputForwardMovement = Input.GetAxis("Vertical");
        inputLateralMovement = Input.GetAxis("Horizontal");
    }

    public void FixedUpdate()
    {
        if (inputForwardMovement != 0 || inputLateralMovement != 0)
        {
            MovePlayer();
            MoveThePaws();
        }
        else
        {
            StopThePaws();
            transform.Rotate(Vector3.zero);
        }
        if (numberOfJumps < 2) Jump();
    }

    private void OnCollisionEnter(Collision other)
    {
        numberOfJumps = 0;
    }

    private void MovePlayer()
    {
        // If The player is in the air, it keeps the vertical movement   
        Vector3 verticalMove = Vector3.up * louseRb.velocity.y;
        Vector3 localDirection = transform.forward;
        Vector3 localMovement = localDirection * inputForwardMovement * moveSpeed;
        louseRb.velocity = localMovement + verticalMove;

        float rotation = inputLateralMovement * rotationSpeed * Time.deltaTime;
        transform.Rotate(Vector3.up * rotation, Space.World);
    }

    private void Jump()
    {
        //Only take impulse in the first jump
        if (Input.GetKeyDown(KeyCode.Space) && numberOfJumps < 1)
        {
            louseBody.position += (Vector3.down * 0.2f);
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {   
            // Only return the body position in the first jump
            if (numberOfJumps < 1) 
            {
                louseBody.position += (Vector3.up * 0.3f);
            }
            //Overrides the current vertical speed before applying the jump.
            louseRb.velocity = new Vector3(louseRb.velocity.x, 0, louseRb.velocity.z);
            louseRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            numberOfJumps += 1;
        }
    }

    private void MoveThePaws()
    {
        for (int i = 0; i < paws.Length; i++)
        {
            paws[i].Walk();
        }
    }

    private void StopThePaws()
    {
        foreach (var paw in paws)
        {
            paw.ReturnToInitialPosition();
        }
    }
}