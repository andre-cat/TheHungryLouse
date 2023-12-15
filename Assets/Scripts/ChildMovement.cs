using UnityEngine;

public class ChildMovement : MonoBehaviour
{
    [Header("Child movement settings")]
    [SerializeField] [Tooltip("Child's movement speed")]
    private float childSpeed;
    [SerializeField] [Tooltip("Distance traveled by the child")]
    private float maxDistance;
    [SerializeField] [Tooltip("Activate this when you want the child to move")]
    private bool isActive = false;
    private Transform louse;
    private Animator childAnimator;
    private Vector3 initialPosition;

    private void Start()
    { 
        initialPosition = transform.position;
        childAnimator = GetComponent<Animator>();
    } 

    private void Update() 
    {
        louse = transform.Find("Louse");
    }   

    private void FixedUpdate()
    {
        if (isActive && childSpeed != 0 && maxDistance != 0)
        {
            MovementHorizontal();
            childAnimator.enabled = true;
        }
        else
        {
            childAnimator.enabled = false;
        }
    }

    private void MovementHorizontal()
    {
        const int direction = -1;
        Vector3 motion = Vector3.forward * childSpeed * Time.deltaTime;
        transform.Translate(motion);
        
        // Detect the limit of the movement
        if (Mathf.Abs(transform.position.x - initialPosition.x) >= maxDistance ||
            Mathf.Abs(transform.position.z - initialPosition.z) >= maxDistance)
        {
            transform.Rotate(0f, 90f, 0f); // Invert the direction
            // The louse is looking dor the same side
            if (louse != null)
            {
                louse.Rotate(0f,90f * direction,0f);
            }
        }
    }

}
