using UnityEngine;
using UnityEngine.InputSystem;

public class ColiderTriggerDetectorByRycast : MonoBehaviour
{
    [SerializeField] private InputActionReference shoot;
    [SerializeField] private float radius = 1.1f;
    [SerializeField] private Canvas canvasInterruptorDetected;
    [SerializeField] private LayerMask layerAllowed;
    // estas layer mask es para comprobar solo una layer, dicha layer la podemos poner en
    // ProjectSettings / physics / Layer Collision Matrix / (desactivar la layer para no tener que calcularla constantemente)

    private bool isOnInterruptor = false;
    
    private void OnEnable()
    {
        shoot.action.Enable();

        shoot.action.started += OnShoot;
    }
    private void Update()
    {
        canvasInterruptorDetected.gameObject.SetActive(false);
        
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, Mathf.Infinity, layerAllowed))
        {
            if (hit.collider.gameObject.tag == "Interruptor")
            {
                canvasInterruptorDetected.gameObject.SetActive(true);
                isOnInterruptor = true;
            }
            else
            {
                isOnInterruptor = false;
            }
        }
        else
        {
            isOnInterruptor = false;
        }

    }

    
    private void OnShoot(InputAction.CallbackContext ctx)
    {
        bool isShooting = ctx.ReadValueAsButton();

        if (isOnInterruptor && isShooting)
        {
            Debug.Log("ACTIVA INTERRUPTOR");
        }
    }

    private void OnDisable()
    {
        shoot.action.Disable();
        
        shoot.action.started -= OnShoot;
    }
}
