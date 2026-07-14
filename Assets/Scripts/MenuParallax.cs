using UnityEngine;
using UnityEngine.InputSystem;

public class MenuParallax : MonoBehaviour
{
    [SerializeField]
    float offsetMultiplier = 1f;

    [SerializeField]
    float smoothTime = 0.3f;

    Vector2 startPosition;
    Vector3 velocity;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        Vector2 offset = Camera.main.ScreenToViewportPoint(Mouse.current.position.value);
        transform.position = Vector3.SmoothDamp(
            transform.position,
            startPosition + (offset * offsetMultiplier),
            ref velocity,
            smoothTime
        );
    }
}
