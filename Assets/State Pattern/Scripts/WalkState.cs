using UnityEngine;
using UnityEngine.InputSystem;

public class WalkState : IStateable
{
    private StateManager character;
    private Animator animator;
    private Rigidbody rb;
    private float moveSpeed = 5f;

    public void OnEnter(Animator animator, StateManager character)
    {
        this.character = character;
        this.animator = animator;
        rb = character.GetComponent<Rigidbody>();

        animator.SetInteger("IntType", 1);
    }

    public void OnUpdate()
    {
        Vector2 input = character.moveAction.ReadValue<Vector2>();
        Move(input);

        if (Keyboard.current.spaceKey.isPressed)
        {
            character.ChangeState(new AttackState());
        }

        if (!Keyboard.current.anyKey.isPressed)
        {
            character.ChangeState(new IdleState());
        }
    }

    public void OnExit()
    {
        animator.SetInteger("IntType", 0);
    }

    public void Move(Vector2 input)
    {
        Vector3 dir = new Vector3(input.x, 0, input.y);

        character.transform.rotation = Quaternion.LookRotation(dir.normalized);

        rb.MovePosition(character.transform.position + dir * moveSpeed * Time.deltaTime);
    }
}
