using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class StateManager : MonoBehaviour
{
    [SerializeField] private StateManager character;
    [SerializeField] private Animator animator;

    private IStateable currentState;

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
