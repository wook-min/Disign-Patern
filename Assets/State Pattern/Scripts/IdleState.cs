using UnityEngine;
using UnityEngine.InputSystem;

public class IdleState : IStateable
{
    private StateManager character;
    private Animator animator;

    public void OnEnter(Animator animator, StateManager character)
    {
        this.character = character;
        this.animator = animator;

        animator.SetInteger("IntType", 0);
    }

    public void OnUpdate() 
    {
        if (Keyboard.current.wKey.isPressed || Keyboard.current.aKey.isPressed ||
            Keyboard.current.sKey.isPressed || Keyboard.current.dKey.isPressed)
        {
            character.ChangeState(new WalkState());
        }

        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            character.ChangeState(new AttackState());
        }
    }
    public void OnExit() { }
}
