using System.Collections;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("Shooting Parameters")]
    [SerializeField]
    float shootingRate;

    [Header("Vision Parameters")]
    [SerializeField]
    float visionRange = 10f;

    [Range(0, 360)]
    [SerializeField]
    float visionAngle = 90f;

    [SerializeField]
    bool startLookingLeft = true;

    [Header("Objects")]
    [SerializeField]
    GameObject bullet;

    [SerializeField]
    Transform shootingPosition;

    [Header("Movement Parameters")]
    [SerializeField]
    bool canWalk = false;

    [SerializeField]
    Transform edgeDetector;

    [SerializeField]
    float edgeDetectorRadius;

    [SerializeField]
    Transform wallDetector;

    [SerializeField]
    float wallDetectorRadius;

    [SerializeField]
    float walkSpeed = 2f;

    [Header("Layers")]
    [SerializeField]
    LayerMask playerLayer;

    [SerializeField]
    LayerMask obstacleLayer;

    [SerializeField]
    LayerMask platformsLayer;

    PlayerController playerController;
    Coroutine shootingCoroutine;
    Rigidbody2D rb;
    float moveDirection;

    void Awake()
    {
        playerController = FindAnyObjectByType<PlayerController>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        shootingCoroutine = StartCoroutine(Shooting());
        moveDirection = startLookingLeft ? -1f : 1f;
    }

    void Update()
    {
        if (IsPlayerInVision())
        {
            Quaternion angle = CalculateAngle();
            shootingPosition.rotation = Quaternion.Euler(
                0,
                0,
                angle.eulerAngles.z - 90 * (startLookingLeft ? 1 : -1)
            );
        }
    }

    void FixedUpdate()
    {
        if (canWalk)
        {
            MoveEnemy();
        }
    }

    void MoveEnemy()
    {
        bool isAtTheEdge = !Physics2D.OverlapCircle(
            edgeDetector.position,
            edgeDetectorRadius,
            platformsLayer
        );
        bool isAtTheWall = Physics2D.OverlapCircle(
            wallDetector.position,
            wallDetectorRadius,
            platformsLayer
        );

        if (isAtTheEdge || isAtTheWall)
        {
            transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
            moveDirection *= -1;
            startLookingLeft = !startLookingLeft;
        }

        rb.linearVelocityX = moveDirection * walkSpeed;
    }

    Quaternion CalculateAngle()
    {
        Vector2 playerPosition = playerController.transform.position;
        Vector2 position = shootingPosition.position;
        Vector2 direction = playerPosition - position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        return Quaternion.AngleAxis(angle - 90, Vector3.forward);
    }

    bool IsPlayerInVision()
    {
        Vector2 playerPosition = playerController.transform.position;
        float distance = Vector2.Distance(playerPosition, shootingPosition.position);
        if (distance > visionRange)
            return false;

        Vector2 directionToPlayer = (
            playerPosition - (Vector2)shootingPosition.position
        ).normalized;
        float angleToPlayer = Vector2.Angle(
            shootingPosition.right * (startLookingLeft ? -1 : 1),
            directionToPlayer
        );
        if (angleToPlayer > visionAngle * 0.5f)
            return false;

        RaycastHit2D hit = Physics2D.Raycast(
            shootingPosition.position,
            directionToPlayer,
            visionRange,
            obstacleLayer | playerLayer
        );
        if (hit.collider != null && ((1 << hit.collider.gameObject.layer) & playerLayer) != 0)
        {
            return true;
        }

        return false;
    }

    IEnumerator Shooting()
    {
        while (true)
        {
            if (IsPlayerInVision())
            {
                Quaternion angle = CalculateAngle();
                Instantiate(bullet, shootingPosition.position, angle);
            }
            yield return new WaitForSeconds(shootingRate);
        }
    }

    public void StopEnemyShootingCoroutine()
    {
        StopCoroutine(shootingCoroutine);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(shootingPosition.position, visionRange);

        Vector3 left =
            Quaternion.Euler(0, 0, -visionAngle * 0.5f)
            * shootingPosition.right
            * (startLookingLeft ? -1 : 1);
        Vector3 right =
            Quaternion.Euler(0, 0, visionAngle * 0.5f)
            * shootingPosition.right
            * (startLookingLeft ? -1 : 1);
        Gizmos.color = Color.red;
        Gizmos.DrawLine(shootingPosition.position, shootingPosition.position + left * visionRange);
        Gizmos.DrawLine(shootingPosition.position, shootingPosition.position + right * visionRange);
    }

    void OnDrawGizmos()
    {
        if (!canWalk)
            return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(edgeDetector.position, edgeDetectorRadius);
        Gizmos.DrawWireSphere(wallDetector.position, wallDetectorRadius);
    }
}
