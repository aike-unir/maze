using UnityEngine;
using UnityEngine.InputSystem;

public class ColiderTriggerDetectorByRycast : MonoBehaviour
{
    [SerializeField] private InputActionReference shoot;
    [SerializeField] private Canvas canvasInterruptorDetected;
    [SerializeField] private LayerMask layerAllowed;
    [SerializeField] private Camera playerCamera;
    // estas layer mask es para comprobar solo una layer, dicha layer la podemos poner en
    // ProjectSettings / physics / Layer Collision Matrix / (desactivar la layer para no tener que calcularla constantemente)

    private bool isOnInterruptor = false;
    private GameObject interruptor;
    
    private void OnEnable()
    {
        shoot.action.Enable();

        shoot.action.started += OnShoot;
    }
    private void Update()
    {
        canvasInterruptorDetected.gameObject.SetActive(false);
        if (Physics.Raycast(transform.position, playerCamera.transform.forward, out RaycastHit hit, Mathf.Infinity, layerAllowed))
        {
            
            
            if (hit.collider.gameObject.tag == "Interruptor")
            {
                canvasInterruptorDetected.gameObject.SetActive(true);
                interruptor = hit.collider.gameObject;
                isOnInterruptor = true;
            }
            else
            {
                interruptor = null;
                isOnInterruptor = false;
            }
        }
        else
        {
            interruptor = null;
            isOnInterruptor = false;
        }

    }

    
    private void OnShoot(InputAction.CallbackContext ctx)
    {
        bool isShooting = ctx.ReadValueAsButton();

        if (isOnInterruptor && isShooting)
        {
            InterruptorController interruptorController = interruptor.GetComponent<InterruptorController>();
            interruptorController.OnTriggerInterruptor();
        }
    }

    private void OnDisable()
    {
        shoot.action.Disable();
        
        shoot.action.started -= OnShoot;
    }
}
