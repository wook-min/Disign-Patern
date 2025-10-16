using UnityEngine;

public class Mushroom : Creature
{
    [SerializeField] private Vector3 originSize;
    [SerializeField] private int maxSize;

    private void Start()
    {
        originSize = transform.localScale;
    }

    public override void Behaviour()
    {
        float offset = Mathf.PingPong(Time.time * speed, maxSize);
        transform.localScale = originSize + new Vector3(offset, offset, offset);
    }
}
