using UnityEngine;

public class FloorSpire : MonoBehaviour
{
    [SerializeField] public float speedMovement = 200f;
    [SerializeField] public float offset = 50f;
    
    private Vector3 targetPosition;
    private Vector3 initialPosition;
    private Vector3 originalPosition;
    
    private bool isActivated = false;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        originalPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        initialPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        targetPosition = new Vector3(transform.position.x, transform.position.y - offset, transform.position.z);


    }

    public void Activate()
    {
        Debug.Log(" === Activate");
        isActivated = true;
    }
    
    public void Deactivate()
    {
        isActivated = false;
        isRetracting = false;
    }

    private bool isRetracting = false;
    // Update is called once per frame
    void Update()
    {
        if (isActivated)
        {
            if (isRetracting)
            {
                transform.position =
                    Vector3.MoveTowards(transform.position, initialPosition, speedMovement * Time.deltaTime);
                
                if (transform.position.y == initialPosition.y)
                {
                    Deactivate();
                }
            }
            else
            {
                transform.position =
                    Vector3.MoveTowards(transform.position, targetPosition, speedMovement * Time.deltaTime);

                if (transform.position.y == targetPosition.y)
                {
                    isRetracting = true;
                }
            }
        }


    }
}
