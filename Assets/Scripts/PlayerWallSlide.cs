using UnityEngine;

public class PlayerWallSlide : MonoBehaviour
{
    [SerializeField]
    float wallDetectorRadius = 0.2f;

    [SerializeField]
    float coyoteTime = 0.15f;

    [SerializeField]
    float wallSlidingSpeed = 2f;

    [SerializeField]
    float wallJumpDuration = 0.1f;

    [SerializeField]
    Vector2 wallJumpingPower = new Vector2(8f, 16f);

    [SerializeField]
    Transform wallDetector;

    [SerializeField]
    LayerMask platformLayer;

    [SerializeField]
    PlayerController playerController;

    bool isWallSliding;
    float wallJumpingDirection;
    float currentCoyoteTime;
    bool isWallJumping;
    Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        WallSlide();
        WallJump();
    }

    void WallSlide()
    {
        if (IsWalled() && !playerController.GetIsGrounded() && !playerController.GetIsMoving())
        {
            isWallSliding = true;
            rb.linearVelocityY = Mathf.Clamp(rb.linearVelocityY, -wallSlidingSpeed, float.MaxValue);
        }
        else
        {
            isWallSliding = false;
        }
    }

    void WallJump()
    {
        if (isWallSliding)
        {
            isWallJumping = false;
            wallJumpingDirection = -transform.localScale.x;
            currentCoyoteTime = coyoteTime;
            CancelInvoke(nameof(StopWallJumping));
        }
        else
        {
            currentCoyoteTime -= Time.deltaTime;
        }
        if (playerController.GetIsJumping() && currentCoyoteTime > 0f)
        {
            isWallJumping = true;
            rb.linearVelocity = new Vector2(
                wallJumpingDirection * wallJumpingPower.x,
                wallJumpingPower.y
            );
            currentCoyoteTime = 0f;

            if (transform.localScale.x != wallJumpingDirection)
            {
                transform.localScale = new Vector2(
                    transform.localScale.x * -1f,
                    transform.localScale.y
                );
            }
            Invoke(nameof(StopWallJumping), wallJumpDuration);
        }
    }

    void StopWallJumping()
    {
        isWallJumping = false;
    }

    public bool GetIsWallJumping()
    {
        return isWallJumping;
    }

    public bool IsWalled()
    {
        return Physics2D.OverlapCircle(wallDetector.position, wallDetectorRadius, platformLayer);
    }

    void OnDrawGizmos()
    {
        if (wallDetector == null)
            return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(wallDetector.position, wallDetectorRadius);
    }
}
