using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private Queue<GameObject> pool = new(10);
    [SerializeField] private int poolCount = 10;

    private static ObjectPool instance;
    public static ObjectPool Instance => instance;


    private void Awake()
    {
        if (instance != null && instance != this)
            Destroy(gameObject);

        instance = this;

        DontDestroyOnLoad(gameObject);

        prefab = Resources.Load<GameObject>("Bee");
    }

    private void Start()
    {
        Create();

        Debug.Log($"[ObjectPool] Queue Count : {pool.Count}");
    }

    public void Create()
    {
        for (int i = 0; i < poolCount; i++)
        {
            var bee = Instantiate(prefab, gameObject.transform);
            bee.SetActive(false);
            pool.Enqueue(bee);
        }
    }

    public GameObject Get(Vector3 position, Quaternion rotation)
    {
        GameObject bee;
        if (pool.Count != 0)
        {
            bee = pool.Dequeue();
        }
        else
        {
            bee = Instantiate(prefab, gameObject.transform);
        }

        bee.SetActive(true);
        bee.transform.position = position;
        bee.transform.rotation = rotation;

        return bee;
    }

    public void ReturnObject(GameObject clone)
    {
        clone.SetActive(false);
        pool.Enqueue(clone);
    }
}
