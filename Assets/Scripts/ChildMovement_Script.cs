using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildMovement_Script : MonoBehaviour
{
    [Header("Child movement settings")]
    [SerializeField] [Tooltip("Child's movement speed")]
    private float speed;
    [SerializeField] [Tooltip("Distance traveled by the child")]
    private float maxDistance;
    [SerializeField] [Tooltip("Child's axis of movement")]
    private Vector3 direction = Vector3.zero;
    private Vector3 initialPosition;
    private float sense = 1.0f;
    private void Start()
    { 
        initialPosition = transform.position;
    } 
    private void FixedUpdate()
    {
        Vector3 motion = direction * sense * speed * Time.deltaTime;
        transform.Translate(motion);
        
        // Detect the limit of the movement
        if (Mathf.Abs(transform.position.x - initialPosition.x) >= maxDistance ||
        Mathf.Abs(transform.position.y - initialPosition.y) >= maxDistance ||
        Mathf.Abs(transform.position.z - initialPosition.z) >= maxDistance)
        {
            sense *= -1.0f; // Invert the direction
        }
    }
}
