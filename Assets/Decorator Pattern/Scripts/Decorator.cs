using UnityEngine;

public abstract class Decorator : IStatus
{
    protected IStatus innerStatus;

    public Decorator(IStatus status)
    {
        innerStatus = status;
    }

    public virtual void OnUpdate()
    {
        innerStatus.OnUpdate();
    }

}
