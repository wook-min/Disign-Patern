using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class CreateManager : MonoBehaviour
{
    [SerializeField] private GameObject prefabs;
    [SerializeField] private float speed = 5f;


    private bool trigger = false;
    private int count = 0;

    private void OnEnable()
    {
        trigger = false;
        count = 0;
        StartCoroutine(CreateDelay());
    }

    private void Update()
    {
        if (Keyboard.current.spaceKey.isPressed)
            Stop();
    }


    private IEnumerator CreateDelay()
    {
        while (true)
        {
            if (trigger)
                yield break;

            if (count <= 20)
            {
                int randomTime = Random.Range(1, 5);
                yield return CoroutineManager.GetCachedWait(randomTime);
            }
            else if (count <= 40)
            {
                int randomTime = Random.Range(1, 3);
                yield return CoroutineManager.GetCachedWait(randomTime);
            }
            else if (count <= 100)
            {
                yield return CoroutineManager.GetCachedWait(0.5f);
            }
            else
            {
                yield return CoroutineManager.GetCachedWait(0.1f);
            }

            var go = Instantiate(prefabs, this.transform);
            go.name = "Bear." + $"{count}";

            float position = Random.Range(-9f, 9f);
            go.transform.position = new Vector3(position, 0, 0);
            go.transform.localRotation = new Quaternion(0, 180, 0, 0);
            go.transform.position -= Vector3.forward * speed * Time.deltaTime;
            Debug.Log("Create Bear");
            count++;
        }
    }

    public void Stop()
    {
        trigger = true;
    }
}
