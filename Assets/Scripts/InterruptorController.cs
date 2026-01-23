using UnityEngine;
using UnityEngine.InputSystem;

public class InterruptorController : MonoBehaviour
{
    [SerializeField] private GameObject[] doors;
    
    public void OnTriggerInterruptor()
    {
        foreach (var door in doors)
        {
            DoorController doorController = door.GetComponent<DoorController>();
            doorController.OpenDoor();
        }
        
    }
}
