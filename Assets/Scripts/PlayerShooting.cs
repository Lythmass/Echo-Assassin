using System.Collections;
using Unity.Mathematics;
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
    Coroutine shoot;

    void Awake()
    {
        attackAction = InputSystem.actions.FindAction("Attack");
        shoot = null;
    }

    void Update()
    {
        if (attackAction.IsPressed() && shoot == null)
        {
            shoot = StartCoroutine(Shoot());
        }
        else if (!attackAction.IsPressed() && shoot != null)
        {
            StopCoroutine(shoot);
            shoot = null;
        }
    }

    IEnumerator Shoot()
    {
        GameObject daggerObject = Instantiate(
            dagger,
            shootingPosition.position,
            quaternion.identity
        );
        int direction = (int)Mathf.Sign(transform.localScale.x);
        daggerObject.transform.rotation = Quaternion.Euler(0, 0, -direction * 90);
        yield return new WaitForSeconds(timeBetweenShots);
    }
}
