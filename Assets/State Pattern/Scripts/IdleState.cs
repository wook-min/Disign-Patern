using UnityEngine;
using UnityEngine.InputSystem;

public class IdleState : IStateable
{
    private StateManager character;
    private Animator animator;

    private float attackBuffer = 0.1f;
    private float bufferTimer = 0f;

    public void OnEnter(Animator animator, StateManager character)
    {
        this.character = character;
        this.animator = animator;

        animator.SetInteger("IntType", 0);

        bufferTimer = 0f;
    }

    public void OnUpdate() 
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            bufferTimer = attackBuffer;
        }

        if (bufferTimer > 0f)
        {
            bufferTimer -= Time.deltaTime;
            character.ChangeState(new AttackState());
            return;
        }

        if (Keyboard.current.wKey.isPressed || Keyboard.current.aKey.isPressed ||
            Keyboard.current.sKey.isPressed || Keyboard.current.dKey.isPressed)
        {
            character.ChangeState(new WalkState());
        }
    }
    public void OnExit() { }
}
