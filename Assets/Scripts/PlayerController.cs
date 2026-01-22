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
    
    private Vector3 velocity;
    private bool isGrounded;
    
    [Header("Movement")]
    [SerializeField] private float speed = 5f;
    [SerializeField] private float runMultiplier = 5f;
    [SerializeField] private float lookSpeed = 1f;
    
    [Header("Jump")]
    [SerializeField] private float jumpHeight = 2f;
    
    [Header("Camera")]
    [SerializeField] private Camera playerCamera;
    
    private CharacterController characterController;
    private Rigidbody playerRigidbody;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        playerRigidbody = GetComponent<Rigidbody>();
    }
    
    private void OnEnable()
    {
        move.action.Enable();
        jump.action.Enable();
        run.action.Enable();

        move.action.started += OnMove;
        move.action.performed += OnMove;
        move.action.canceled += OnMove;

        jump.action.started += OnJump;
        
        run.action.started += OnRun;
        run.action.performed += OnRun;
        run.action.canceled += OnRun;
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
        
        move.action.Disable();
        jump.action.Disable();
        run.action.Disable();
    }
}
