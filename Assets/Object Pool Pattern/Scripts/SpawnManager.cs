using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private float radius = 10f; // 원 반지름
    [SerializeField] private GameObject characterPosition;

    private float randomAngle;
    private WaitForSeconds ws = new(0.5f);


    private void Start()
    {
        StartCoroutine(SpawnBee());
    }

    private IEnumerator SpawnBee()
    {

        while (true)
        {
            randomAngle = Random.Range(0f, 360f);
            float rad = randomAngle * Mathf.Deg2Rad;
            Vector3 direction = new Vector3(Mathf.Cos(rad), 0, Mathf.Sin(rad));

            // Random.insideUnitCircle << 랜덤 원주 잡아줌.

            Vector3 position = characterPosition.transform.position + direction * radius;

            Quaternion dir = Quaternion.LookRotation(position - characterPosition.transform.position);

            var bee = ObjectPool.Instance.Get(position, dir);

            if (bee.TryGetComponent<Bee>(out var Bee))
            {
                Bee.Init(characterPosition);
            }

            yield return ws;
        }
    }

}
