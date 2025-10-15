using Unity.VisualScripting;
using UnityEngine;

public class Golem : Creature
{
    [SerializeField] private Transform endPositionMinus; // true
    [SerializeField] private Transform endPositionPlus; // false

    private bool check;
    private Transform current;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        current = endPositionMinus;
        check = true;
    }

    public override void Behaviour()
    {
        if (Vector3.Distance(gameObject.transform.position, current.position) > 0.1f)
        {
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position,
                current.position, speed * Time.deltaTime);
            animator.SetInteger("state", 1);
        }
        else
        {
            if (check)
            {
                check = !check;
                current = endPositionPlus;
            }
            else
            {
                check = !check;
                current = endPositionMinus;
            }
            animator.SetInteger("state", 0);
        }
    }
}
