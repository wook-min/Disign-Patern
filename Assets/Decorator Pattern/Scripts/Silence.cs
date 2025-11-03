using UnityEngine;

public class Silence : Decorator
{
    public Silence(IStatus status) : base(status) { }

    public override void OnUpdate()
    {
        base.OnUpdate();

        Debug.Log("Silence State");
    }
}
