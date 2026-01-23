using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class DoorController : MonoBehaviour
{
    
    [SerializeField] public float speed = 2f;
    [SerializeField] public float offset = 5f;

    public enum DoorSide
    {
        DoorLeft,
        DoorRight
    }

    [SerializeField] private DoorSide doorSize;

    private Vector3 finalPosition;
    
    public void Start()
    {
        if (doorSize == DoorSide.DoorLeft) {
            finalPosition = transform.position + new Vector3(-offset, 0f, 0f);
        }
        else
        {
            finalPosition = transform.position + new Vector3(offset, 0f, 0f);
        }
       
    }
    
    private bool openDoor = false;
    public void OpenDoor()
    {
        openDoor = true;
        Debug.Log(" === Door is opening");
    }

    public void Update()
    {
        if (openDoor)
        {
            transform.position = Vector3.MoveTowards(transform.position, finalPosition, speed * Time.deltaTime);
        }
    }

}
