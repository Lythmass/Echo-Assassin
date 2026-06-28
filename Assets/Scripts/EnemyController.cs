using System.Collections;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("Shooting Parameters")]
    [SerializeField]
    float shootingRate;

    [SerializeField]
    float shootingRange;

    [Header("Objects")]
    [SerializeField]
    GameObject bullet;

    [SerializeField]
    Transform shootingPosition;

    PlayerController playerController;
    Coroutine shootingCoroutine;

    void Awake()
    {
        playerController = FindAnyObjectByType<PlayerController>();
    }

    void Start()
    {
        shootingCoroutine = StartCoroutine(Shooting());
    }

    Quaternion CalculateAngle()
    {
        Vector2 playerPosition = playerController.transform.position;
        Vector2 position = shootingPosition.position;
        Vector2 direction = playerPosition - position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        return Quaternion.AngleAxis(angle - 90, Vector3.forward);
    }

    float CalculateDistance()
    {
        Vector2 playerPosition = playerController.transform.position;
        Vector2 position = transform.position;
        return Vector2.Distance(playerPosition, position);
    }

    IEnumerator Shooting()
    {
        while (true)
        {
            float distance = CalculateDistance();
            if (distance <= shootingRange)
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
}
