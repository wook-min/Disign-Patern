using UnityEngine;

public abstract class Creature : MonoBehaviour
{
    [SerializeField] protected float speed;

    public abstract void Behaviour();

    public void Update()
    {
        if (GameManager.Instance.State == false) return;

        Behaviour();
    }
}
