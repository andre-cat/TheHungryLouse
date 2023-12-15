using UnityEngine;

public class WinGirlController : MonoBehaviour
{

    [SerializeField] private float speed;
    [SerializeField] private Transform firstTransform;
    [SerializeField] private Transform lastTransform;

    private Vector3 initRotation;
    private int direction;

    private void Start()
    {
        initRotation = transform.eulerAngles;
        direction = +1;
    }

    private void FixedUpdate()
    {
        if (transform.position.x < firstTransform.position.x)
        {
            direction = +1;
            transform.rotation = Quaternion.Euler(initRotation.x, initRotation.y, initRotation.z);
        }
        else if (transform.position.x >= lastTransform.position.x)
        {
            direction = -1;
            transform.rotation = Quaternion.Euler(initRotation.x, -initRotation.y, initRotation.z);
        }

        transform.position = transform.position + new Vector3(speed * direction * Time.deltaTime, 0, 0);
    }
}
