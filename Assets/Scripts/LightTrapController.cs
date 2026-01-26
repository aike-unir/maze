using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class LightTrapController : MonoBehaviour
{
    [SerializeField] private Material notGlowingMaterial;
    [SerializeField] private Material glowingMaterial;
    
    [SerializeField] private GameObject[] traps;

    private void Awake()
    {
        MeshRenderer mr = GetComponent<MeshRenderer>();
        mr.material = notGlowingMaterial;
    }
    
    public void ActivateLightTrap()
    {
        MeshRenderer mr = GetComponent<MeshRenderer>();
        mr.material = glowingMaterial;
        
        foreach (var trap in traps)
        {
            Debug.Log(" === Activate LightTrap");
            FloorSpiresManager trapController = trap.GetComponent<FloorSpiresManager>();
            trapController.ActivateTrap();
        }

    }
    
    public void DeactivateLightTrap()
    {
        MeshRenderer mr = GetComponent<MeshRenderer>();
        mr.material = notGlowingMaterial;

    }
}
