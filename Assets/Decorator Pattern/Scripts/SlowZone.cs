using UnityEngine;

public class SlowZone : MonoBehaviour
{
    [SerializeField] private Decorator slow;

    private void Awake()
    {
        slow = GetComponent<Decorator>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.TryGetComponent<player>(out var player))
        {
            slow.Activate();
        }
    }
}
