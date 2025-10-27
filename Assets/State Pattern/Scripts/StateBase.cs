using UnityEngine;

public abstract class StateBase
{
    [SerializeField] private GameObject character;
    [SerializeField] private Animator animator;

    public StateBase(GameObject character, Animator animator)
    {
        this.character = character;
        this.animator = animator;
    }
}
