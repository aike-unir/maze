using UnityEngine;

public class ColiderTriggerDetectorByRycast : MonoBehaviour
{
    [SerializeField] private float radius = 1.1f;
    [SerializeField] private Canvas canvasInterruptorDetected;
    [SerializeField] private LayerMask layerAllowed;
    // estas layer mask es para comprobar solo una layer, dicha layer la podemos poner en
    // ProjectSettings / physics / Layer Collision Matrix / (desactivar la layer para no tener que calcularla constantemente)
    
    private void Update()
    {
        canvasInterruptorDetected.gameObject.SetActive(false);
        
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, Mathf.Infinity, layerAllowed))
        {
            if (hit.collider.gameObject.tag == "Interruptor")
            {
                canvasInterruptorDetected.gameObject.SetActive(true);
            }
        }

    }
}
