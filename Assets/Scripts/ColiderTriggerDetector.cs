using UnityEngine;

public class ColiderTriggerDetector : MonoBehaviour
{
    [SerializeField] private float radius = 1.1f;
    [SerializeField] private Canvas canvasTrapDetected;
    [SerializeField] private LayerMask layerAllowed;
    // estas layer mask es para comprobar solo una layer, dicha layer la podemos poner en
    // ProjectSettings / physics / Layer Collision Matrix / (desactivar la layer para no tener que calcularla constantemente)
    
    private void Update()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius, layerAllowed);
        bool anyTrapDetected = false;
        foreach (Collider col in colliders)
        {
            
            if (col.tag == "Trap")
            {
                anyTrapDetected = true;
                Debug.Log("Hay un trigger trampa cerca");
            }
            
        }

        canvasTrapDetected.gameObject.SetActive(anyTrapDetected);

    }
}
