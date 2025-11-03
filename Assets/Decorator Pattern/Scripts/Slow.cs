using UnityEngine;

public class Slow : Decorator
{
    public Slow(IStatus status) : base(status) { }

    public override void OnUpdate()
    {
        base.OnUpdate();
        Debug.Log("Slow State");
    }
}
