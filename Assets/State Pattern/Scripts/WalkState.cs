using UnityEngine;
using UnityEngine.InputSystem;

public class WalkState : IStateable
{
    private StateManager character;
    private Animator animator;
    private float moveSpeed = 5f;

    public void OnEnter(Animator animator, StateManager character)
    {
        this.character = character;
        this.animator = animator;

        animator.SetInteger("IntType", 1);
    }

    public void OnUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Move(new Vector2(h, v));

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
        if (input.sqrMagnitude < 0.001f)
            return;

        Transform cam = Camera.main.transform;
        Vector3 camForward = Vector3.Scale(cam.forward, new Vector3(1, 0, 1)).normalized;
        Vector3 camRight = cam.right;

        Vector3 moveDir = (camForward * input.y + camRight * input.x).normalized;

        character.transform.rotation = Quaternion.Slerp
            (character.transform.rotation, Quaternion.LookRotation(moveDir), 0.15f);

        character.transform.position += moveDir * moveSpeed * Time.deltaTime;
    }
}
