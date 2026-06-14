using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    [SerializeField]
    PlayerController playerController;
    Animator animator;
    bool hasLanded = true;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (playerController.GetWasPressedJump() && playerController.HasJumped())
        {
            animator.SetTrigger("isJumping");
        }

        if (!playerController.GetIsGrounded())
        {
            hasLanded = false;
        }
        else
        {
            if (!hasLanded)
            {
                animator.SetTrigger("isLanding");
                hasLanded = true;
            }
        }
    }
}
