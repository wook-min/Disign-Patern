using UnityEngine;

public class FearEffect : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<player>(out var player))
        {
            player.SetMove(false);
        }
    }
}
