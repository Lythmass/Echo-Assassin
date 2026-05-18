using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Player Configs")]
    [SerializeField]
    float speed;

    [SerializeField]
    float jumpForce;

    [Header("Mechanic Configs")]
    [SerializeField]
    float fallGravityMultiplier;

    [SerializeField]
    float jumpGravityMultiplier;

    [SerializeField]
    float coyoteTime;
    float currentCoyoteTime;

    [Header("Ground Detection")]
    [SerializeField]
    Transform groundDetector;

    [SerializeField]
    float radius;
    bool isGrounded;
    InputAction moveAction;
    InputAction jumpAction;
    Vector2 moveInput;
    Rigidbody2D rb;
    LayerMask platformLayer;

    void Awake()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        jumpAction = InputSystem.actions.FindAction("Jump");
        platformLayer = LayerMask.GetMask("Platform");
        rb = GetComponent<Rigidbody2D>();
        currentCoyoteTime = coyoteTime;
    }

    void Update()
    {
        moveInput = moveAction.ReadValue<Vector2>();
        isGrounded = Physics2D.OverlapCircle(groundDetector.position, radius, platformLayer);
        if (!isGrounded)
        {
            currentCoyoteTime -= Time.deltaTime;
        }
        else
        {
            currentCoyoteTime = coyoteTime;
        }
    }

    void FixedUpdate()
    {
        rb.linearVelocityX = moveInput.x * speed;
        Jump();
    }

    void Jump()
    {
        if (jumpAction.IsPressed() && currentCoyoteTime > Mathf.Epsilon)
        {
            rb.linearVelocityY = jumpForce;
            currentCoyoteTime = 0f;
            isGrounded = false;
        }

        if (rb.linearVelocityY < 0)
        {
            rb.linearVelocityY +=
                (fallGravityMultiplier - 1) * Physics2D.gravity.y * Time.fixedDeltaTime;
        }
        else if (rb.linearVelocityY > 0 && !jumpAction.IsPressed())
        {
            rb.linearVelocityY +=
                (jumpGravityMultiplier - 1) * Physics2D.gravity.y * Time.fixedDeltaTime;
        }
    }

    void OnDrawGizmos()
    {
        if (groundDetector == null)
            return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundDetector.position, radius);
    }
}
