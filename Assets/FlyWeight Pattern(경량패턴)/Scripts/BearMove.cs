using Unity.VisualScripting;
using UnityEngine;

public class BearMove : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private Animator animator;

    private bool trigger = false;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        trigger = false;
        animator.SetInteger("IntType", 1);
    }

    void Update()
    {
        if (trigger)
        {
            return;
        }

        transform.position -= Vector3.forward * speed * Time.deltaTime;
    }

    public void Stop()
    {
        trigger = true;
    }
}
