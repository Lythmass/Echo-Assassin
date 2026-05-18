using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    float speed;

    [SerializeField]
    float jumpForce;

    InputAction moveAction;
    InputAction jumpAction;
    Vector2 moveInput;
    Rigidbody2D rb;

    void Awake()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        jumpAction = InputSystem.actions.FindAction("Jump");
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        moveInput = moveAction.ReadValue<Vector2>();
    }

    void FixedUpdate()
    {
        rb.linearVelocityX = moveInput.x * speed;
        Jump();
    }

    void Jump()
    {
        if (jumpAction.IsPressed())
        {
            rb.linearVelocityY = jumpForce;
        }
    }
}
