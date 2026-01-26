using UnityEngine;

public class MoveSpire : MonoBehaviour
{
    [SerializeField] public float speedMovement = 200f;
    [SerializeField] public SpireDirection firstDirection;
    [SerializeField] public float offset = 50f;
    
    private Vector3 targetPosition;
    private Vector3 initialPosition;
    
    
    public enum SpireDirection
    {
        MoveLeft,
        MoveRight
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        initialPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);

        if (firstDirection == SpireDirection.MoveLeft)
        {
            targetPosition = new Vector3(transform.position.x + offset, transform.position.y, transform.position.z);
        }
        else
        {
            targetPosition = new Vector3(transform.position.x - offset, transform.position.y, transform.position.z);
        }

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speedMovement * Time.deltaTime);
            if (transform.position.x == targetPosition.x)
            {
                Vector3 aux = initialPosition;
                initialPosition = targetPosition;
                targetPosition = aux;
            }


    }
}
