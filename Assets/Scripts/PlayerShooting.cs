using System.Collections;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField]
    GameObject dagger;

    [SerializeField]
    Transform shootingPosition;

    [SerializeField]
    float timeBetweenShots;

    InputAction attackAction;
    Vector2 startShootPosition;
    Vector2 endShootPosition;

    bool canPlayerShoot;
    bool isHoldingDownMouse;

    void Awake()
    {
        attackAction = InputSystem.actions.FindAction("Attack");
        canPlayerShoot = true;
    }

    void Update()
    {
        if (attackAction.IsPressed() && canPlayerShoot && !isHoldingDownMouse)
        {
            isHoldingDownMouse = true;
            startShootPosition = Mouse.current.position.value;
        }
        if (!attackAction.IsPressed() && isHoldingDownMouse)
        {
            isHoldingDownMouse = false;
            endShootPosition = Mouse.current.position.value;
            Shoot();
            SetCanPlayerShoot(false);
        }
    }

    public bool SetCanPlayerShoot(bool value)
    {
        canPlayerShoot = value;
        return canPlayerShoot;
    }

    void Shoot()
    {
        Vector2 direction = endShootPosition - startShootPosition;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Instantiate(dagger, shootingPosition.position, Quaternion.Euler(0, 0, angle - 90));
    }
}
