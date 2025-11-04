using UnityEngine;

public abstract class Decorator : Debuff
{
    [SerializeField] protected player player;
    protected Debuff debuff;

    private void Awake()
    {
        player = GameObject.Find("Character").GetComponent<player>();
    }

    public override void Activate()
    {
        debuff.Activate();
    }

}
