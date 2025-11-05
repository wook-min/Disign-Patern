using System.Collections;
using UnityEngine;

public class Bee : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private Animator animator;

    private WaitForSeconds ws = new(1.5f);
    public Animator Animator => animator;
    
    private GameObject destination;
    private Coroutine moveCoroutine;

    public bool IsMoving { get; private set; }

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        IsMoving = true;
        SetAnimator(0);
    }

    private void OnDisable()
    {
        if (moveCoroutine != null)
        {
            StopCoroutine(moveCoroutine);
            moveCoroutine = null;
        }
    }

    public void Init(GameObject go)
    {
        destination = go;
        SetMove(true);

        if (moveCoroutine != null)
            StopCoroutine(moveCoroutine);

        moveCoroutine = StartCoroutine(Move());
    }

    private IEnumerator Move()
    {
        Vector3 direction = destination.transform.position - transform.position;
        SetAnimator(1);
        transform.rotation = Quaternion.LookRotation(direction);

        while (IsMoving)
        {
            transform.position += direction.normalized * speed * Time.deltaTime;
            yield return null;
        }
    }

    public void DelayDie()
    {
        StartCoroutine(Die());
    }

    private IEnumerator Die()
    {
        SetAnimator(2);

        yield return ws;

        SetMove(false);
        SpawnManager.Instance.ReturnToPool(gameObject);
    }

    public void SetMove(bool isOn)
    {
        IsMoving = isOn;
    }

    public void SetAnimator(int index)
    {
        animator.SetInteger("State", index);
    }

}
