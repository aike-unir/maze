using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.InputSystem;

public class InterruptorController : MonoBehaviour
{
    [SerializeField] private Material notGlowingMaterial;
    [SerializeField] private Material glowingMaterial;
    [SerializeField] private GameObject[] doors;
    [SerializeField] [CanBeNull] private GameObject floorTraps;
    
    private void Awake()
    {
        MeshRenderer mr = GetComponent<MeshRenderer>();
        mr.material = notGlowingMaterial;
    }
    
    public void OnTriggerInterruptor()
    {
        foreach (var door in doors)
        {
            DoorController doorController = door.GetComponent<DoorController>();
            doorController.OpenDoor();
            
            MeshRenderer mr = GetComponent<MeshRenderer>();
            mr.material = glowingMaterial;
        }

        if (floorTraps != null)
        {
            LightTrapManager lightTrapManager = floorTraps?.GetComponent<LightTrapManager>();
            lightTrapManager?.ActivateTraps();
        }
    }
}
