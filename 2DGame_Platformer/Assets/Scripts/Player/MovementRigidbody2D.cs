using UnityEngine;

public class MovementRigidbody2D : MonoBehaviour
{
    [Header("LayerMask")]
    [SerializeField]
    private LayerMask groundCheckLayer; // 바닥 체크를 위한 충돌 레이어

    [Header("Move")]
    [SerializeField]
    private float walkSpeed = 5.0f;     // 걷는 속도
    [SerializeField]
    private float runSpeed = 8.0f;      // 뛰는 속도

    [Header("Jump")]
    [SerializeField]
    private float jumpForce = 13.0f;        // 점프 힘
    [SerializeField]
    private float lowGravityScale = 2.0f;   // 점프키를 오래 누르고 있을 때 적용되는 중력(높은 점프)
    [SerializeField]
    private float highGravityScale = 3.5f;  // 일반적으로 적용되는 중력 (낮은 점프)

    private float moveSpeed;    // 이동 속도

    // 바닥에 착지 직전 조금 빨리 점프 키를 눌렀을 때 바닥에 착지하면 바로 점프가 되도록
    private float jumpBufferTime = 0.1f;
    private float jumpBufferCounter;

    // 낭떠러지에서 떨어질 때 아주 잠시 동안 점프가 가능하도록 설정하기 위한 변수
    private float hangTime = 0.2f;
    private float hangCounter;

    private Vector2 collisionSize;  // 머리, 발 위치에 생성하는 충돌 박스 크기
    private Vector2 footPosition;   // 발 위치

    private Rigidbody2D rigid2D;    // 물리를 제어하는 컴포넌트
    private Collider2D collider2D;  // 현재 오브젝트의 충돌 범위

    public bool IsLongJump { set; get; } = false;           // 낮은 점프, 높은 점프 체크
    public bool IsGrounded { private set;get; } = false;    // 바닥 체크 (바닥에 닿아있을 때 true)

    public Vector2 Velocity => rigid2D.linearVelocity;

    private void Awake()
    {
        moveSpeed = walkSpeed;

        rigid2D = GetComponent<Rigidbody2D>();
        collider2D = GetComponent<Collider2D>();
    }

    private void Update()
    {
        UpdateCollision();
        JumpHeight();
        JumpAdditive();
    }

    /// <summary>
    /// x 축 속력(velocity) 설정. 외부 클래스에서 호출
    /// </summary>
    public void MoveTo(float x)
    {
        // x의 절대값이 0.5이면 걷기(walkSpeed), 1이면 뛰기(runSpeed)
        moveSpeed = Mathf.Abs(x) != 1 ? walkSpeed : runSpeed;

        // x가 -0.5, 0.5의 값을 가질 때 x를 -1, 1로 변경
        if(x != 0)x = Mathf.Sign(x);

        // x축 방향 속력을 x * moveSpeed로 설정
        rigid2D.linearVelocity = new Vector2(x * moveSpeed, rigid2D.linearVelocity.y);
    }

    private void UpdateCollision()
    {
        // 플레이어 오브젝트의 Collider2D min, center, max 위치 정보
        Bounds bounds = collider2D.bounds;

        // 플레이어의 발에 생성하는 충돌 범위
        collisionSize = new Vector2((bounds.max.x - bounds.min.x) * 0.5f,0.1f);

        // 플레이어의 발 위치
        footPosition = new Vector2(bounds.center.x, bounds.min.y);

        // 플레이어가 바닥을 밟고 있는지 체크하는 충돌 박스
        IsGrounded = Physics2D.OverlapBox(footPosition, collisionSize, 0, groundCheckLayer);
    }

    /// <summary>
    /// 다른 클래스에서 호출하는 점프 메소드
    /// y축 점프
    /// </summary>
    public void Jump()
    {
        jumpBufferCounter = jumpBufferTime;
    }

    private void JumpHeight()
    {
        // 낮은 점프, 높은 점프를 구현을 위한 중력 계수(gravityScale) 조절 (Jump Up일 때만 적용된다)
        // 중력 계수가 낮은 if 문은 높은 점프가 되고, 중력 계수가 높은 else 문은 낮은 점프가 된다
        if(IsLongJump && rigid2D.linearVelocity.y >0)
        {
            rigid2D.gravityScale = lowGravityScale;
        }
        else
        {
            rigid2D.gravityScale = highGravityScale;
        }
    }

    private void JumpAdditive()
    {
        // 낭떠러지에서 떨어질 때 아주 잠시동안은 점프가 가능하도록 설정
        if (IsGrounded) hangCounter = hangTime;
        else hangCounter -= Time.deltaTime;

        // 바닥에 착지 직전 조금 빨리 점프 키를 눌렀을 때 바닥에 착지하면 바로 점프
        if(jumpBufferCounter > 0) jumpBufferCounter -= Time.deltaTime;

        if(jumpBufferCounter > 0 && hangCounter > 0)
        {
            // 점프 힘(jumpForce)만큼 y축 방향 속력으로 설정
            rigid2D.linearVelocity = new Vector2(rigid2D.linearVelocity.x, jumpForce);
            jumpBufferCounter = 0;
            hangCounter = 0;
        }
    }
}
