using System.Collections;
using UnityEngine;
using UnityEngine.WSA;

public class Slow : Decorator
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

    public override void Activate()
    {
        if (isCoolTime) return;

        player.SetMove(false);
        StartCoroutine(slowEffect(player, slowTime));


        Debug.Log("Slow State");
    }

    private IEnumerator slowEffect(player player, float delay)
    {
        speedOrigin = player.Speed;
        player.SetSpeed(player.Speed * slowAmount);
        lastEffectTime = Time.time;
        Debug.Log("Slow Effect");
        yield return ws;

        player.SetSpeed(speedOrigin);
    }
}
