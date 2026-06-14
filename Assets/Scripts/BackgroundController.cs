using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    float startPosX;
    float startPosY;
    float length;

    [SerializeField]
    GameObject cam;

    [SerializeField]
    float parallaxEffect;

    void Start()
    {
        startPosX = transform.position.x;
        startPosY = transform.position.y;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void Update()
    {
        float distance = cam.transform.position.x * parallaxEffect;
        float movement = cam.transform.position.x * (1 - parallaxEffect);
        transform.position = new Vector3(
            startPosX + distance,
            startPosY + cam.transform.position.y,
            transform.position.z
        );

        if (movement > startPosX + length)
        {
            startPosX += length;
        }
        else if (movement < startPosX - length)
        {
            startPosX -= length;
        }
    }
}
