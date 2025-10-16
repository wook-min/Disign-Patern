using Unity.VisualScripting;
using UnityEngine;

public class Golem : Creature
{
    [SerializeField] private Transform endPositionMinus; // true
    [SerializeField] private Transform endPositionPlus; // false
    [SerializeField] private Vector3 originPosition;

    private bool check;
    private Transform current;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        originPosition = transform.position;
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
    /// <sumery>
    /// 다른 방식
    /// float offset = Mathf.PingPong(Time.time * speed, targetY);
    /// transform.position = originPosition + new Vector3(0, offset, 0);
    /// </sumery>>
}
