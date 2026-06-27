using Cinemachine;
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
    float slamThreshold = -60f;

    [Header("Camera Follow")]
    [SerializeField]
    CinemachineVirtualCamera cinemachineVirtualCamera;

    [SerializeField]
    CameraShakeSO cameraShakeSO;

    [SerializeField]
    float leftOffset = 0.35f;

    [SerializeField]
    float rightOffset = 0.65f;
    CinemachineFramingTransposer cinemachineFramingTransposer;
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
        cinemachineFramingTransposer =
            cinemachineVirtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>();
    }

    void Update()
    {
        moveInput = moveAction.ReadValue<Vector2>();
        Flip();
        isGrounded = Physics2D.OverlapCircle(groundDetector.position, radius, platformLayer);
        if (jumpAction.WasPressedThisFrame() && currentCoyoteTime > Mathf.Epsilon)
        {
            AudioManager.instance.PlayJumpSFX();
        }
        if (!isGrounded)
        {
            hasLanded = false;
            currentCoyoteTime -= Time.deltaTime;
            maxFallSpeedBeforeLanding = Mathf.Min(maxFallSpeedBeforeLanding, rb.linearVelocityY);
        }
        else
        {
            currentCoyoteTime = coyoteTime;
            if (!hasLanded)
            {
                if (maxFallSpeedBeforeLanding <= slamThreshold)
                {
                    AudioManager.instance.PlayHeavyLandSFX();
                    maxFallSpeedBeforeLanding = 0f;
                    CameraShake.Instance.ShakeCamera(
                        cameraShakeSO.GetIntensity(),
                        cameraShakeSO.GetFrequency(),
                        cameraShakeSO.GetDuration()
                    );
                    landingEffect.Play();
                }
                else
                {
                    AudioManager.instance.PlayLandSFX();
                }

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
        SetCameraHorizontalOffset(direction);
    }

    public void SetCameraHorizontalOffset(int direction)
    {
        if (direction == 1)
        {
            cinemachineFramingTransposer.m_ScreenX = leftOffset;
        }
        else
        {
            cinemachineFramingTransposer.m_ScreenX = rightOffset;
        }
    }

    public bool GetIsGrounded()
    {
        return isGrounded;
    }

    public bool GetIsJumping()
    {
        return jumpAction.IsPressed();
    }

    public bool GetWasPressedJump()
    {
        return jumpAction.WasPressedThisFrame();
    }

    public bool GetIsMoving()
    {
        return moveInput.x != 0;
    }

    public bool HasJumped()
    {
        return currentCoyoteTime > Mathf.Epsilon;
    }

    public void ResetVelocity()
    {
        rb.linearVelocity = Vector2.zero;
    }

    void OnDrawGizmos()
    {
        if (groundDetector == null)
            return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundDetector.position, radius);
    }
}
