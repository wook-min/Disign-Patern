using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float bulletSpeed = 5f;
    [SerializeField] private float maxTime = 10f;

    private float shootTime = 0f;

    public void Init(Transform position)
    {
        gameObject.transform.position = position.position;
    }

    public void StartCo()
    {
        StartCoroutine(BulletMove());
    }


    private IEnumerator BulletMove()
    {
        Vector3 velocity = bulletSpeed * Vector3.forward;
        Vector3 pos = gameObject.transform.position;
        

        while (true)
        {
            shootTime += Time.deltaTime;
            float xMove = bulletSpeed * shootTime;
            float yDrop = -(0.5f * 9.806f * shootTime * shootTime);

            transform.position = pos + (velocity.normalized * xMove) + new Vector3(0, yDrop, 0);
            // Translate(Vector3.forward * speed * Time.deltaTime);

            if (shootTime >= maxTime)
            {
                Destroy(gameObject);
                yield break;
            }

            yield return null;
        }
    }
}
