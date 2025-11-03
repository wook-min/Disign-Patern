using UnityEngine;

public class SilenceEffect : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<player>(out var player))
        {
            player.SetMove(false);
        }
    }
}
