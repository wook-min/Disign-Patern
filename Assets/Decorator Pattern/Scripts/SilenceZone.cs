using UnityEngine;

public class SilenceZone : MonoBehaviour
{
    [SerializeField] private Decorator silence;

    private void Awake()
    {
        silence = GetComponent<Decorator>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.TryGetComponent<player>(out var player))
        {
            silence.Activate();
        }
    }
}
