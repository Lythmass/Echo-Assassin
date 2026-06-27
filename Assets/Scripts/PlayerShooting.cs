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
    TimeManager timeManager;

    [SerializeField]
    InputAction attackAction;
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
            timeManager.SlowTimeDown();
            isHoldingDownMouse = true;
        }
        if (!attackAction.IsPressed() && isHoldingDownMouse)
        {
            isHoldingDownMouse = false;
            endShootPosition = Mouse.current.position.value;
            Shoot();
            SetCanPlayerShoot(false);
        }
        if (!isHoldingDownMouse)
        {
            timeManager.ResetTime();
        }
    }

    public bool SetCanPlayerShoot(bool value)
    {
        canPlayerShoot = value;
        return canPlayerShoot;
    }

    void Shoot()
    {
        AudioManager.instance.PlayDaggerShootSFX();
        Vector2 basePosition = Camera.main.WorldToScreenPoint(transform.position);
        Vector2 direction = endShootPosition - basePosition;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Instantiate(dagger, shootingPosition.position, Quaternion.Euler(0, 0, angle - 90));
    }
}
