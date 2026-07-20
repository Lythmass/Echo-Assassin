using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAim : MonoBehaviour
{
    [SerializeField]
    float rayDistance = 100f;

    [SerializeField]
    Transform shootingPosition;

    [SerializeField]
    LineRenderer lineRenderer;

    [SerializeField]
    LayerMask castLayers;

    [SerializeField]
    PlayerShooting playerShooting;

    Pause pauseManager;

    void Awake()
    {
        pauseManager = FindAnyObjectByType<Pause>();
    }

    void Update()
    {
        if (pauseManager.GetIsPaused())
            return;
        if (playerShooting.GetIsHoldingDownMouse())
        {
            CastRay();
        }
        else
        {
            lineRenderer.SetPosition(1, Vector2.zero);
        }
    }

    void CastRay()
    {
        Vector2 direction = CalculateRayDirection();
        RaycastHit2D hit = Physics2D.Raycast(
            shootingPosition.position,
            direction,
            rayDistance,
            castLayers
        );
        if (hit)
        {
            Vector2 hitPoint = hit.point - (Vector2)transform.position;
            hitPoint = new Vector2(hitPoint.x * Mathf.Sign(transform.localScale.x), hitPoint.y);
            lineRenderer.SetPosition(1, hitPoint);
        }
        else
        {
            direction = new Vector2(direction.x * Mathf.Sign(transform.localScale.x), direction.y);
            lineRenderer.SetPosition(1, direction * rayDistance);
        }
    }

    Vector2 CalculateRayDirection()
    {
        Vector2 mousePos = Mouse.current.position.value;
        Vector2 basePos = Camera.main.WorldToScreenPoint(transform.position);
        Vector2 direction = (mousePos - basePos).normalized;

        return direction;
    }
}
