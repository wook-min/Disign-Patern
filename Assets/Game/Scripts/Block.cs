using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField] private float movingSpeed1 = 5f;
    [SerializeField] private float movingSpeed2 = 10f;
    [SerializeField] private float movingSpeed3 = 15f;
    [SerializeField] private Rigidbody rb;

    private float currentMove;
    private bool stop = false;
    public bool IsActive { get; set; } = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        rb.useGravity = false;
    }

    public void Init(float size, MoveSpeed speed, Vector3 position)
    {
        transform.localScale = new Vector3(0.21f, 0.15f, size);
        transform.position = position;
        switch (speed)
        {
            case MoveSpeed.LEVEL1:
                currentMove = movingSpeed1;
                break;
            case MoveSpeed.LEVEL2:
                currentMove = movingSpeed2;
                break;
            case MoveSpeed.LEVEL3:
                currentMove = movingSpeed3;
                break;
            default:
                currentMove = movingSpeed1;
                break;
        }

        StartCoroutine(moveBlock());
    }

    // a가 밑, b가 위
    public float ChangeOverlap(Transform A, Transform B)
    {
        float offset = Mathf.Abs(A.position.x - B.position.x);
        float size = A.localScale.z; // 기준 블록 길이 (같다고 가정)

        // 겹친 길이 = 전체 길이 - 중심 차이
        float overlap = size - offset;

        if (overlap <= 0f)
        {
            Debug.Log("Game Over: 완전히 빗나감");
            return 0f;
        }

        // 방향 판별 (왼쪽 or 오른쪽)
        bool isRight = B.position.x > A.position.x;

        // 위치 재조정 (겹친 부분 중앙으로)
        float moveAmount = offset / 2f;
        if (isRight)
            B.position -= new Vector3(moveAmount, 0, 0);
        else
            B.position += new Vector3(moveAmount, 0, 0);

        // ✅ Z축 길이에서 offset만큼 빼준다 (겹친 부분만 남김)
        B.localScale = new Vector3(
            B.localScale.x,
            B.localScale.y,
            overlap
        );

        return offset;
    }

    private IEnumerator moveBlock()
    {
        Debug.Log("코루틴 시작");
        Vector3 left = new Vector3(-2f, transform.position.y, 0);
        Vector3 right = new Vector3(2f, transform.position.y, 0);
        bool check = true;


        while (!stop)
        {
            if (check)
            {
                transform.position = Vector3.MoveTowards(transform.position, left,
                    currentMove * Time.deltaTime);

                if (Vector3.Distance(transform.position, left) < 0.1f)
                {
                    check = false;
                }
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, right,
                    currentMove * Time.deltaTime);

                if (Vector3.Distance(transform.position, right) < 0.1f)
                {
                    check = true;
                }
            }

            yield return null;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (!IsActive) return;

        if (other.transform.CompareTag("Player"))
        {
            BlockSpawner.Instance.SetSize(ChangeOverlap(other.transform, transform));
        }

        if (other.transform.CompareTag("Floor"))
        {
            BlockSpawner.Instance.SetGameOver();
        }
    }

    public void SetStop()
    {
        stop = true;
        rb.useGravity = true;
    }

   
}

public enum MoveSpeed
{
    LEVEL1,
    LEVEL2,
    LEVEL3
}
