using System;
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
    
    
    Vector2 rawMovement = Vector2.zero;
    
    private Vector3 velocity;
    private bool isGrounded;
    float lookHorizontal = 0f;
    float lookVertical = 0f;
    float verticalRotation;
    
    [Header("Movement")]
    [SerializeField] private float speed = 5f;
    [SerializeField] private float runMultiplier = 5f;
    [SerializeField] private float lookSpeed = 1f;
    
    [Header("Jump")]
    [SerializeField] private float jumpHeight = 2f;
    
    [Header("Camera")]
    [SerializeField] private Camera playerCamera;
    [SerializeField] private float maxAngleUp = 80f;
    [SerializeField] private float maxAngleDown = -80f;
    
    private CharacterController characterController;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }
    
    private void OnEnable()
    {
        move.action.Enable();
        jump.action.Enable();
        run.action.Enable();
        look.action.Enable();

        move.action.started += OnMove;
        move.action.performed += OnMove;
        move.action.canceled += OnMove;

        jump.action.started += OnJump;
        
        run.action.started += OnRun;
        run.action.performed += OnRun;
        run.action.canceled += OnRun;
        
        look.action.started += OnLook;
        look.action.performed += OnLook;
        look.action.canceled += OnLook;
    }

    // Events
    private void OnMove(InputAction.CallbackContext ctx)
    {
        rawMovement = ctx.ReadValue<Vector2>();
    }

    private bool mustJump = false;
    private void OnJump(InputAction.CallbackContext ctx)
    {
        mustJump = ctx.ReadValueAsButton();
        // Debug.Log($"mustJump: {mustJump}");
    }

    private bool mustRun= false;
    private void OnRun(InputAction.CallbackContext ctx)
    {
        mustRun = ctx.ReadValueAsButton();
    }

    private void OnLook(InputAction.CallbackContext ctx)
    {
        lookHorizontal = ctx.ReadValue<Vector2>().x;
        lookVertical = ctx.ReadValue<Vector2>().y;
    }
    
    // Update
    private void Update()
    {
        isGrounded = characterController.isGrounded;
        
        float realSpeed = speed;

        if (mustRun)
        {
            realSpeed = speed * runMultiplier;
        }
        
        Vector3 moveToApply = new Vector3(rawMovement.x, 0 , rawMovement.y);
        moveToApply = transform.TransformDirection(moveToApply);  // Importante para mover tras rotación
        characterController.Move(moveToApply * realSpeed * Time.deltaTime);
        
        
        // Jump
        if (mustJump && isGrounded)
        {
            velocity.y = Mathf.Sqrt(2f * Mathf.Abs(-Physics.gravity.y) * jumpHeight);
            mustJump = false;
        }
        
        // Aplicar gravedad
        velocity.y += Physics.gravity.y * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);
        
        // Look
        transform.Rotate(Vector3.up, lookHorizontal, Space.World);
        characterController.transform.Rotate(Vector3.up, lookHorizontal, Space.Self);
        
        
        verticalRotation = Mathf.Clamp(verticalRotation - (lookVertical * lookSpeed), maxAngleDown, maxAngleUp);
        playerCamera.transform.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);

    }
    
    private void OnDisable()
    {
        move.action.started -= OnMove;
        move.action.performed -= OnMove;
        move.action.canceled -= OnMove;

        jump.action.started -= OnJump;
        
        run.action.started -= OnRun;
        run.action.performed -= OnRun;
        run.action.canceled -= OnRun;
        
        look.action.started -= OnLook;
        look.action.performed -= OnLook;
        look.action.canceled -= OnLook;
        
        move.action.Disable();
        jump.action.Disable();
        run.action.Disable();
        look.action.Disable();
    }
}
