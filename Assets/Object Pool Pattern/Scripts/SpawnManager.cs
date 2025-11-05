using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private float radius = 10f; // 원 반지름
    [SerializeField] private GameObject characterPosition;
    [SerializeField] private int poolCount = 10;

    private static SpawnManager instance;
    public static SpawnManager Instance => instance;

    private Queue<GameObject> pool = new();
    

    private float randomAngle;
    private WaitForSeconds ws = new(0.5f);

    private void Awake()
    {
        if (instance != null && instance != this)
            Destroy(gameObject);

        instance = this;

        DontDestroyOnLoad(gameObject);


        for (int i = 0; i < poolCount; i++)
        {
            var bee = Instantiate(prefab, gameObject.transform);
            bee.SetActive(false);
            pool.Enqueue(bee);
        }
    }

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

            var bee = Get();

            bee.SetActive(true);
            bee.transform.position = position;
            bee.transform.rotation = dir;

            if (bee.TryGetComponent<Bee>(out var Bee))
            {
                Bee.Init(characterPosition);
            }
            yield return ws;
        }
    }

    public GameObject Get()
    {
        if (pool.Count != 0)
        {
            return pool.Dequeue();
        }
        else
        {
            var bee = Instantiate(prefab, gameObject.transform);
            return bee;
        }
    }

    public void ReturnToPool(GameObject go)
    {
        pool.Enqueue(go);
        go.SetActive(false);   
    }

}
