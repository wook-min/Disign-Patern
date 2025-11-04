using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class player : Debuff
{
    [Header("캐릭터 이동 관련")]
    [SerializeField] private float speed = 5f; // 이동속도
    public float Speed => speed;
    [SerializeField] private float stopDistance = 0.1f; // 목표 지점이 멈춰야할 거리(부동소수점 고려)
    [SerializeField] private bool isMoving = false;
    [SerializeField] private Vector3 targetPosition;
    [SerializeField] private float rotationSpeed = 10f; // 회전 속도
    [SerializeField] private Rigidbody rb;
    
    private RaycastHit rayhit;
    private bool isControl = true;

   // [Header("디버프 관련")]

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        isControl = true;
    }



    private void Update()
    {
        if (isControl)
        {
            if (Mouse.current.leftButton.isPressed)
            {
                ClickCoordinate();
            }
        }
    }

    private void FixedUpdate()
    {
        if (isMoving)
        {
            Move();
        }
    }

    public void ClickCoordinate()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out rayhit, Mathf.Infinity, LayerMask.NameToLayer("Move")))
        {
            targetPosition = rayhit.point;
            isMoving = true;
        }
    }

    private void Move()
    {
        // 목표 지점 방향 계산
        Vector3 direction = (targetPosition - transform.position);

        direction.y = 0f; // y축 고정

        float distance = direction.magnitude; // 벡터 길이 계산

        // 도착 확인
        if (distance <= stopDistance)
        {
            isMoving = false;
            rb.linearVelocity = Vector3.zero;
            return;
        }

        Vector3 newPosition = rb.position + direction.normalized * speed * Time.fixedDeltaTime;

        rb.MovePosition(newPosition);
        Quaternion targetRotation = Quaternion.LookRotation(direction) * Quaternion.Euler(0, 180f,0);
        Quaternion smoothRotation = Quaternion.Slerp(rb.rotation, targetRotation,
            rotationSpeed * Time.fixedDeltaTime);

        rb.MoveRotation(smoothRotation);
        
    }

    public void SetMove(bool isMove)
    {
        isMoving = isMove;
    }

    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed;
    }

    public void SetControl(bool isControl)
    {
        this.isControl = isControl;
    }

    public override void Activate()
    {
        Debug.Log("Character");
    }
}
