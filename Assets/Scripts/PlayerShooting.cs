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
    float knockBackDuration;
    float knockBackTimer;

    [SerializeField]
    InputAction attackAction;
    Vector2 endShootPosition;
    Rigidbody2D rb;
    Vector2 enemyKilledPosition;

    [SerializeField]
    Vector2 knockBackForce = new Vector2(10f, 40f);

    bool hasKilledEnemy = false;
    bool startTimer;

    bool canPlayerShoot;
    bool isHoldingDownMouse;

    PlayerDeath playerDeath;

    void Awake()
    {
        attackAction = InputSystem.actions.FindAction("Attack");
        rb = GetComponent<Rigidbody2D>();
        canPlayerShoot = true;
        playerDeath = GetComponent<PlayerDeath>();
    }

    void Update()
    {
        if (!playerDeath.GetIsAlive())
            return;
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

        if (startTimer)
        {
            knockBackTimer -= Time.deltaTime;
        }
        if (knockBackTimer <= 0f)
        {
            startTimer = false;
        }
    }

    void FixedUpdate()
    {
        if (!playerDeath.GetIsAlive())
            return;
        KnockBackOnKill();
    }

    void KnockBackOnKill()
    {
        if (!hasKilledEnemy)
            return;
        int direction = transform.position.x > enemyKilledPosition.x ? 1 : -1;
        Vector2 knockBackVector = new Vector2(direction, 1f);
        rb.linearVelocity = Vector2.zero;
        rb.AddForce(knockBackVector * knockBackForce, ForceMode2D.Impulse);
        hasKilledEnemy = false;
    }

    public bool SetCanPlayerShoot(bool value)
    {
        canPlayerShoot = value;
        return canPlayerShoot;
    }

    public void KillAnEnemy(Vector2 position)
    {
        startTimer = true;
        hasKilledEnemy = true;
        knockBackTimer = knockBackDuration;
        enemyKilledPosition = position;
    }

    public bool GetIsBeingKnockedBack()
    {
        return knockBackTimer > 0f;
    }

    void Shoot()
    {
        AudioManager.instance.PlayDaggerShootSFX();
        Vector2 basePosition = Camera.main.WorldToScreenPoint(transform.position);
        Vector2 direction = endShootPosition - basePosition;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Instantiate(dagger, shootingPosition.position, Quaternion.Euler(0, 0, angle - 90));
    }

    public float GetKnockBackTimer()
    {
        return knockBackTimer;
    }

    public bool GetIsHoldingDownMouse()
    {
        return isHoldingDownMouse;
    }
}
