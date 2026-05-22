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

    [SerializeField]
    PlayerWallSlide playerWallSlide;
    float currentCoyoteTime;

    [Header("Ground Detection")]
    [SerializeField]
    Transform groundDetector;

    [SerializeField]
    float radius;

    [Header("Landing Effect")]
    [SerializeField]
    ParticleSystem landingEffect;

    [SerializeField]
    CameraShake cameraShake;

    [SerializeField]
    float slamThreshold = -60f;

    bool isGrounded;
    bool hasLanded;
    float maxFallSpeedBeforeLanding;
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
        Flip();
        isGrounded = Physics2D.OverlapCircle(groundDetector.position, radius, platformLayer);
        if (!isGrounded)
        {
            hasLanded = false;
            currentCoyoteTime -= Time.deltaTime;
            maxFallSpeedBeforeLanding = Mathf.Min(maxFallSpeedBeforeLanding, rb.linearVelocityY);
        }
        else
        {
            currentCoyoteTime = coyoteTime;
            if (!hasLanded && maxFallSpeedBeforeLanding <= slamThreshold)
            {
                maxFallSpeedBeforeLanding = 0f;
                cameraShake.SlamShake();
                landingEffect.Play();
                hasLanded = true;
            }
        }
    }

    void FixedUpdate()
    {
        if (!playerWallSlide.GetIsWallJumping())
        {
            rb.linearVelocityX = moveInput.x * speed;
        }
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

    void Flip()
    {
        bool isMoving = Mathf.Abs(moveInput.x) > Mathf.Epsilon;
        if (!isMoving || playerWallSlide.GetIsWallJumping())
            return;
        int direction = (int)Mathf.Sign(moveInput.x);
        transform.localScale = new Vector2(direction, transform.localScale.y);
    }

    public bool GetIsGrounded()
    {
        return isGrounded;
    }

    public bool GetIsJumping()
    {
        return jumpAction.IsPressed();
    }

    public bool GetIsMoving()
    {
        return moveInput.x != 0;
    }

    void OnDrawGizmos()
    {
        if (groundDetector == null)
            return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundDetector.position, radius);
    }
}
