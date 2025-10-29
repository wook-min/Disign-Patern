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

        animator.applyRootMotion = false;
        animator.ResetTrigger("Attack"); // << 이거 넣으니 해결됨. 왜?
        animator.SetTrigger("Attack");
    }

    public void OnExit()
    {
        animator.SetInteger("IntType", 0);
        animator.applyRootMotion = true;
    }

    public void OnUpdate()
    {
        info = animator.GetCurrentAnimatorStateInfo(0);

        if (info.IsName("Attack") && !animator.IsInTransition(0))
        {
            character.ChangeState(new IdleState());
        }
    }
}
