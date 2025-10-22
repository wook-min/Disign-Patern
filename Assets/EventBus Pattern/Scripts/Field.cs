using Unity.VisualScripting;
using UnityEngine;

public class Field : MonoBehaviour
{
    [SerializeField] Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        EventManager.Subscribe(Condition.START, Execute);
        EventManager.Subscribe(Condition.PAUSE, Pause);
        EventManager.Subscribe(Condition.FINISH, Exit);
    }

    private void OnDisable()
    {
        EventManager.UnSubscribe(Condition.START, Execute);
        EventManager.UnSubscribe(Condition.PAUSE, Pause);
        EventManager.UnSubscribe(Condition.FINISH, Exit);
    }

    public void Pause()
    {
        animator.enabled = false;
    }

    public void Execute()
    {
        animator.enabled = true;
    }

    public void Exit()
    {
        Destroy(gameObject);
    }
}
