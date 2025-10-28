using Unity.VisualScripting;
using UnityEngine;

public class AttackState : IStateable
{
    private StateManager character;
    private Animator animator;
    private AnimatorStateInfo info;

    public void OnEnter(Animator animator, StateManager character)
    {
        this.character = character;
        this.animator = animator;

        info = this.animator.GetCurrentAnimatorStateInfo(0);
        if (!animator.IsInTransition(0) && !info.IsName("Attack"))
        {
            this.animator.SetTrigger("Attack");
        }
    }

    public void OnExit()
    {
        
    }

    public void OnUpdate()
    {
        if (info.normalizedTime < 0.0001f)
        {
            character.ChangeState(new IdleState());
        }
    }
}
