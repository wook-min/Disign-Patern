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
        if (animator.GetInteger(animationName) == 2)
            return;

        if(Keyboard.current.spaceKey.IsPressed())
        {
            Attack();
        }

    }

    public void Attack()
    {
        animator.SetInteger(animationName, 2);
    }
}
