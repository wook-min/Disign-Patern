using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class StateManager : MonoBehaviour
{
    [SerializeField] private StateManager character;
    [SerializeField] private Animator animator;

    public InputAction moveAction; // GetAxis를 대체하는 InputSystem용 이벤트

    private IStateable currentState;

    private void OnEnable()
    {
        moveAction.Enable();
    }

    private void OnDisable()
    {
        moveAction.Disable();
    }

    private void Start()
    {
        ChangeState(new IdleState());
    }

    private void Update()
    {
        currentState?.OnUpdate();
    }

    public void ChangeState(IStateable newState)
    {
        currentState?.OnExit();
        currentState = newState;
        currentState?.OnEnter(animator, character);
    }
}
