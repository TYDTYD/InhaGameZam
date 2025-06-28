using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeMonster : MonoBehaviour
{

    [Header("수치 설정")]
    public float patrolSpeed = 1f;
    public float maxPatrolDistance = 7f;
    public float detectRange = 5f;
    public float chaseSpeed = 3f;
    public float chaseRange = 3f;
    public int attackDamage = 2;
    public float attackCooldown = 1f;


    public Transform groundCheck;
    public Transform wallCheck;
    public LayerMask groundLayer;
    

    private Rigidbody2D rb;
    private Transform player;
    private Vector2 lastPosition;
    private float moveDistance = 0f;
    private bool movingRight = true;

    private IPlayerDetector detector;
    private IChaseBehavior chaser;
    private IAttackBehavior attacker;
    private IWatchBehavior watcher;

    private enum MonsterState { Patrol, Watch, Chase }
    private MonsterState state = MonsterState.Patrol;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        lastPosition = transform.position;

        // 전략 할당 (슬라임은 단순 추적 + 몸통박치기)
        detector = new HorizontalRangeDetector(detectRange, transform);
        chaser = new FullChase(chaseSpeed);
        attacker = new TouchAttack(attackDamage, attackCooldown);
        watcher = new PassiveWatch(chaseRange);
    }

    void Update()
    {
        if (player == null) return;

        bool detected = detector.IsPlayerDetected(transform, player);

        switch (state)
        {
            case MonsterState.Patrol:
                Patrol(); // 순찰 상태일 때 Patrol 함수 호출
                if (detected) // 플레이어가 감지 범위에 들어오면 감시 상태로 전환
                {
                    watcher.OnEnterWatch();
                    state = MonsterState.Watch;
                }
                break;
            case MonsterState.Watch:
                watcher.Watch(transform, player, rb);
                if (!detected)
                {
                    state = MonsterState.Patrol;
                }
                else if (watcher.ShouldChase(transform, player))
                {
                    state = MonsterState.Chase;
                }
                break;
            case MonsterState.Chase:
                chaser.Chase(transform, player, rb);
                attacker.Attack(transform, player);

                if (!detected)
                {
                    watcher.OnEnterWatch();
                    state = MonsterState.Watch;
                }
                break;
        }
    }

    void Patrol()
    {
        float dir = movingRight ? 1f : -1f;
        rb.velocity = new Vector2(dir * patrolSpeed, rb.velocity.y);

        Vector2 groundCheckPos = groundCheck.position + Vector3.right * (movingRight ? 0.3f : -0.3f);
        RaycastHit2D groundHit = Physics2D.Raycast(groundCheckPos, Vector2.down, 0.1f, groundLayer);
        RaycastHit2D wallHit = Physics2D.Raycast(wallCheck.position, Vector2.right * (movingRight ? 1f : -1f), 0.1f, groundLayer);

        float moved = Vector2.Distance(transform.position, lastPosition);
        moveDistance += moved;
        lastPosition = transform.position;

        if (!groundHit.collider || wallHit.collider || moveDistance >= maxPatrolDistance)
        {
            Flip();
            moveDistance = 0f;
        }

        Debug.DrawRay(groundCheckPos, Vector2.down * 0.1f, Color.red);
        Debug.DrawRay(wallCheck.position, Vector2.right * (movingRight ? 0.1f : -0.1f), Color.blue);
    }

    void Flip()
    {
        movingRight = !movingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

}