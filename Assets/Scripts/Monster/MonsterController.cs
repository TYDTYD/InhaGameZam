using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterControl : MonoBehaviour
{
    public enum MonsterState    //몬스터 상태
    {
        Patrol,    // 순찰 상태 
        Watch,     // 감시 상태
        Chase,     // 추적 상태
    }

    public float speed = 2f; // Speed of the monster
    public Transform groundCheck;     //바닥 감지
    public Transform wallCheck;       //벽 감지
    public LayerMask groundLayer;     // Layer for ground detection
    

    // 몬스터 감지용 변수
    public float watchRange = 5f; // 플레이어 감지 범위(감시용)
    public float chaseRange = 3f; // 플레이어 감지 범위(추적용)
    public float chaseSpeed = 3f; // 추적 속도
    
    public float damagecooldown = 1f; // 공격 쿨타임

    private MonsterState state = MonsterState.Patrol; // 현재 상태
    private bool movingDirectionRight = true;

    private Rigidbody2D rb;
    private Vector2 lastPosition;

    private float moveDistance = 0f;  // 이동 거리 측정용
    private float maxDistance = 5f;    // 얼마나 가면 돌아설지 (설정 가능)

    private Transform player;         // 플레이어 위치 (추적용)
    private float lastAttackTime = -999f; // 마지막 공격 시간 (공격 쿨타임용)
    private MonsterStats stats;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();   
        stats = GetComponent<MonsterStats>(); // 몬스터 스탯 컴포넌트 가져오기
        lastPosition = transform.position; // 현재 위치 저장
        player = GameObject.FindGameObjectWithTag("Player").transform; // 플레이어 위치 가져오기
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs(transform.position.y - player.position.y) > 1.0f)
        {
            Patrol(); // 플레이어가 너무 높거나 낮으면 순찰 상태 유지
            return; // 플레이어가 너무 높거나 낮으면 무시
        }
        float distanceToPlayer = Mathf.Abs(transform.position.x - player.position.x); // 플레이어와의 거리 계산
        switch (state)
        {
            case MonsterState.Patrol:
                Patrol(); // 순찰 상태일 때 Patrol 함수 호출
                if (distanceToPlayer < watchRange) // 플레이어가 감지 범위에 들어오면 감시 상태로 전환
                {
                    ChangeState(MonsterState.Watch);
                }
                break;
            case MonsterState.Watch:
                Watch(); // 감시 상태일 때 Watch 함수 호출
                if (distanceToPlayer < chaseRange)
                {
                    ChangeState(MonsterState.Chase); // 플레이어가 추적 범위에 들어오면 추적 상태로 전환
                }
                else if (distanceToPlayer >= watchRange) // 플레이어가 감지 범위를 벗어나면 순찰 상태로 돌아감
                {
                    ChangeState(MonsterState.Patrol);
                }
                break;
            case MonsterState.Chase:
                Chase(); // 추적 상태일 때 Chase 함수 호출
                if (distanceToPlayer >= watchRange) // 플레이어가 감지 범위를 벗어나면 감시 상태로 전환
                {
                    ChangeState(MonsterState.Watch);
                }
                break;
        }

    }
    void Patrol()
    {
        //이동방향 조절
        float dir = movingDirectionRight ? 1f : -1f;
        rb.velocity = new Vector2(dir * speed, rb.velocity.y);

        //바닥 체크
        Vector2 groundCheckPos = groundCheck.position + Vector3.right * (movingDirectionRight ? 0.3f : -0.3f);
        RaycastHit2D groundHit = Physics2D.Raycast(groundCheckPos, Vector2.down, 0.1f, groundLayer);
        bool isGround = groundHit.collider != null;

        //벽 체크
        RaycastHit2D wallHit = Physics2D.Raycast(wallCheck.position, Vector2.right * (movingDirectionRight ? 1f : -1f), 0.1f, groundLayer);
        bool isWall = wallHit.collider != null;

        //이동 거리 체크

        float distanceMoved = Vector2.Distance(transform.position, lastPosition);
        moveDistance += distanceMoved;
        lastPosition = transform.position;

        if (!isGround || isWall || moveDistance >= maxDistance)
        {
            Flip();
            moveDistance = 0f;
        }

        // 디버그용 레이 시각화
        Debug.DrawRay(groundCheckPos, Vector2.down * 0.1f, Color.red);
        Debug.DrawRay(wallCheck.position, Vector2.right * (movingDirectionRight ? 0.1f : -0.1f), Color.blue);
    }
    void ChangeState(MonsterState newState)
    {
        state = newState;
    }
    void Watch()
    {
        if (player.position.x < transform.position.x && movingDirectionRight)
            Flip();
        else if (player.position.x > transform.position.x && !movingDirectionRight)
            Flip();
    }

    void Chase()
    {
        if (player.position.x < transform.position.x && movingDirectionRight)
            Flip();
        else if (player.position.x > transform.position.x && !movingDirectionRight)
            Flip();

        Vector2 groundCheckPos = groundCheck.position + Vector3.right * (movingDirectionRight ? 0.3f : -0.3f);
        RaycastHit2D groundHit = Physics2D.Raycast(groundCheckPos, Vector2.down, 0.1f, groundLayer);
        bool isGround = groundHit.collider != null;

        RaycastHit2D wallHit = Physics2D.Raycast(wallCheck.position, Vector2.right * (movingDirectionRight ? 1f : -1f), 0.1f, groundLayer);
        bool isWall = wallHit.collider != null;

        if (!isGround || isWall)
        {
            rb.velocity = Vector2.zero; // 멈추기
            return; // 더 이상 쫓지 않음
        }

        Vector2 dir = (player.position - transform.position).normalized;
        rb.velocity = new Vector2(dir.x * chaseSpeed, rb.velocity.y);
    }

    void Flip()
    {
        movingDirectionRight = !movingDirectionRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1; // x축 반전
        transform.localScale = scale;

    }
}
