using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class CreateManager : MonoBehaviour
{
    [SerializeField] private GameObject prefabs;
    [SerializeField] private float time = 5.0f;

    private WaitForSeconds seconds;
    private bool trigger = false;
    private int count = 0;

    private void OnEnable()
    {
        trigger = false;
        count = 0;
        seconds = new(time);
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

            yield return seconds;

            var go = Instantiate(prefabs, this.transform);
            go.name = "Bear." + $"{count}";
            go.transform.position = new Vector3(-5 + count, 0, 0);
            go.transform.localRotation = new Quaternion(0, 180, 0, 0);
            Debug.Log("Create Bear");
            count++;
        }
    }

    public void Stop()
    {
        trigger = true;
    }
}
