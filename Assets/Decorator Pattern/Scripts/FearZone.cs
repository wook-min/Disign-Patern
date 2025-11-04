using UnityEngine;

public class FearZone : MonoBehaviour
{
    [SerializeField] private Decorator fear;

    private void Awake()
    {
        fear = GetComponent<Decorator>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.TryGetComponent<player>(out var player))
        {
            fear.Activate();
        }
    }
}
