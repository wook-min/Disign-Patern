using System.Collections;
using UnityEngine;

public class Fear : Decorator
{
    [SerializeField] private float fearTime = 3f;
    [SerializeField] private float fearSpeed = 5f;
    [SerializeField] private float effectCooltime = 5f;
    [SerializeField] private float rotationSpeed = 30f;


    private float lastEffectTime = -999f;
    private bool isCoolTime => Time.time < lastEffectTime + effectCooltime;


    public override void Activate()
    {
        if (isCoolTime) return;

        StartCoroutine(FearEffect(player.gameObject));
        
        Debug.Log("Fear");
    }


    private IEnumerator FearEffect(GameObject go)
    {
        if (!go.TryGetComponent<player>(out var player)) yield break;
        if (!go.TryGetComponent<Rigidbody>(out var rb)) yield break;

        player.SetControl(false);

        Quaternion reverseRotation = Quaternion.LookRotation(-go.transform.forward);
        Vector3 backwardDir = reverseRotation * Vector3.forward;

        float timer = 0f;
        while (timer < fearTime)
        {
            // 자연스러운 회전
            rb.rotation = Quaternion.Lerp(rb.rotation, reverseRotation, rotationSpeed * Time.deltaTime);

            // 반대 방향으로 이동
            rb.MovePosition(rb.position + backwardDir * fearSpeed * Time.deltaTime);

            timer += Time.deltaTime;
            yield return null;
        }

        player.SetControl(true);
    }
}
