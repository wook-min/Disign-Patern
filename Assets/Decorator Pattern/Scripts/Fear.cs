using UnityEngine;

public class Fear : Decorator
{
    public Fear(IStatus status) : base(status) { }

    public override void OnUpdate()
    {
        base.OnUpdate();
        Debug.Log("Fear State");
    }
}
