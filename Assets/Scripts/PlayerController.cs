using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements.Experimental;

public class PlayerController : MonoBehaviour
{
    [Header("Input Actions")] 
    [SerializeField] private InputActionReference move;
    [SerializeField] private InputActionReference look;
    [SerializeField] private InputActionReference jump;
    [SerializeField] private InputActionReference run;
    [SerializeField] private InputActionReference shoot;
    
    Vector2 rawMovement = Vector2.zero;
    
    [Header("Movement")]
    [SerializeField] private float speed = 5f;
    [SerializeField] private float lookSpeed = 1f;
    
    [Header("Camera")]
    [SerializeField] private Camera playerCamera;

    private void OnEnable()
    {
        move.action.Enable();

        move.action.started += OnMove;
        move.action.performed += OnMove;
        move.action.canceled += OnMove;
    }

    // Events
    private void OnMove(InputAction.CallbackContext ctx)
    {
        rawMovement = ctx.ReadValue<Vector2>();
    }

    // Update
    private void Update()
    {
        float moveSpeed = speed * Time.deltaTime;
        Vector3 moveToApply = new Vector3(rawMovement.x * moveSpeed, 0, rawMovement.y * moveSpeed);
        transform.Translate(moveToApply);
    }
    
    private void OnDisable()
    {
        move.action.started -= OnMove;
        move.action.performed -= OnMove;
        move.action.canceled -= OnMove;
    }
}
