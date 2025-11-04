using UnityEngine;

public class Silence : Decorator
{

    public override void Activate()
    {
        gameObject.SetActive(false);
    }
}
