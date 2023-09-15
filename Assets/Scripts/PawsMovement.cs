using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawsMovement : MonoBehaviour
{
    [SerializeField]
    private float pawsSpeed = 3f;
    [SerializeField]
    private float maxRotation = 2f;
    private Vector3 initialRotation;
    private void Start()
    { 
        initialRotation = transform.rotation.eulerAngles;
    } 

    public void Walk()
    {
        Vector3 pawsRotation = Vector3.right * pawsSpeed * Time.deltaTime;
        transform.Rotate(pawsRotation);
        
        if ((transform.rotation.eulerAngles.x - initialRotation.x) >= maxRotation )
        {
            pawsSpeed *= -1; 
        }
    }

    public void ReturnToInitialPosition()
    {
        Vector3 rotation = transform.rotation.eulerAngles;
        transform.Rotate(-rotation.x,0,0);
    }
}
