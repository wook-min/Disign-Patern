using UnityEngine;
using UnityEngine.InputSystem;

public class Character : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private string animationName = "IntType";

    private int currentInt;

    private void Awake()
    {
        currentInt = 0;
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if(Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            Attack();
        }
    }

    public void Attack()
    {
        AnimatorStateInfo animatorStateInfo = animator.GetCurrentAnimatorStateInfo(0);

        if (!animator.IsInTransition(0) && !animatorStateInfo.IsName("Attack"))
        { 
            animator.SetTrigger("Attack");
            Debug.Log("Attack");
        }
        else
        {
            return;
        }
    }
}
