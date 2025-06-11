using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;
    public float groundDrag;
    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    public float landingGravityMultiplier;
    bool readyToJump;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    public bool grounded;

    public Transform orientation;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;
    Rigidbody rb;

    private PlayerControls controls;
    private Vector2 moveInput;
    private bool jumpInput;
    Animator animator;

    void Awake()
    {
        controls = new PlayerControls();
        controls.PlayerInput.Movement.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        controls.PlayerInput.Movement.canceled += ctx => moveInput = Vector2.zero;
        controls.PlayerInput.Jump.performed += ctx => jumpInput = true;
        controls.PlayerInput.Jump.canceled += ctx => jumpInput = false;
    }

    void OnEnable() => controls.PlayerInput.Enable();
    void OnDisable() => controls.PlayerInput.Disable();

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        readyToJump = true;
        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        Vector3 rayOrigin = transform.position + Vector3.up * (playerHeight * 0.5f);
        float rayLength = playerHeight * 0.5f + 0.2f;
        grounded = Physics.Raycast(rayOrigin, Vector3.down, rayLength, whatIsGround);

        // Draw the ray in green if grounded, red if not
        Color rayColor = grounded ? Color.green : Color.red;
        Debug.DrawRay(rayOrigin, Vector3.down * rayLength, rayColor);

        MyInput();
        SpeedControl();

        animator?.SetFloat("speed", new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z).magnitude);
        animator?.SetBool("isGrounded", grounded);

        rb.linearDamping = grounded ? groundDrag : groundDrag * 0.5f;
    }

    void FixedUpdate() => MovePlayer();

    private void MyInput()
    {
        horizontalInput = moveInput.x;
        verticalInput = moveInput.y;

        if (jumpInput && readyToJump && grounded)
        {
            readyToJump = false;
            Jump();
            Invoke(nameof(ResetJump), jumpCooldown);
        }
    }

    private void MovePlayer()
    {
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        if (grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed, ForceMode.VelocityChange);
        else
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * airMultiplier, ForceMode.VelocityChange);
            rb.AddForce(Vector3.down * landingGravityMultiplier, ForceMode.Acceleration);
        }
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
        if (flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.linearVelocity = new Vector3(limitedVel.x, rb.linearVelocity.y, limitedVel.z);
        }
    }

    private void Jump()
    {
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        animator?.SetTrigger("jump");
    }

    private void ResetJump() => readyToJump = true;
}