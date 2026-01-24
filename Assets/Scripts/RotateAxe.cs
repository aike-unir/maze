using UnityEngine;

public class RotateAxe : MonoBehaviour
{
    [SerializeField] public float speedRotation = 200f;
    [SerializeField] public AxeDirection firstDirection;
    
    private Quaternion targetRotationLeft;
    private Quaternion targetRotationRight;
    private AxeDirection axeDirection;
    
    public enum AxeDirection
    {
        RotateLeft,
        RotateRight
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        targetRotationLeft = Quaternion.Euler(0f, 0f, 90f);
        targetRotationRight = Quaternion.Euler(0f, 0f, 270f);
        axeDirection = firstDirection;
    }

    // Update is called once per frame
    void Update()
    {
        if (axeDirection == AxeDirection.RotateLeft)
        {
            if (transform.rotation.eulerAngles.z <= 90f)
            {
                axeDirection = AxeDirection.RotateRight;
            }
            else
            {
                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotationLeft, speedRotation * Time.deltaTime);
            }
            
        }
        else
        {
            if (transform.rotation.eulerAngles.z >= 270f)
            {
                axeDirection = AxeDirection.RotateLeft;
            }
            else
            {
                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotationRight, speedRotation * Time.deltaTime);
            }
        }
        
    }
}
