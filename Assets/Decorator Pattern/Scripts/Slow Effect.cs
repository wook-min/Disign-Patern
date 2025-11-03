using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class SlowEffect : MonoBehaviour
{
    [SerializeField] private float slowTime = 5f;
    [SerializeField] private float slowAmount = 0.5f;
    [SerializeField] private float effectCooltime = 5f;
    
    private float speedOrigin;
    private WaitForSeconds ws;

    private float lastEffectTime = -999f;
    private bool isCoolTime => Time.time < lastEffectTime + effectCooltime;


    private void Awake()
    {
        ws = new(slowTime);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (!isCoolTime) return;

        if (other.TryGetComponent<player>(out var player))
        {
            player.SetMove(false);
            StartCoroutine(slowEffect(player, slowTime));
        }
    }


    private IEnumerator slowEffect(player player, float delay)
    {
        speedOrigin = player.Speed;
        player.SetSpeed(player.Speed * slowAmount);
        lastEffectTime = Time.time;
        yield return ws;

        player.SetSpeed(speedOrigin);
    }
}
