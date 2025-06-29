using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotMonster : MonoBehaviour
{

    [SerializeField] ObjectPooling objectPool;

    [Header("수치 설정")]
    public float patrolSpeed = 1f;
    public float maxPatrolDistance = 7f;
    public float detectRange = 20f;
    public float stopRange = 19f;
    public float chaseSpeed = 1f;
    public float chaseRange = 20f;
    public int attackDamage = 30;
    public float attackCooldown = 5f;


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
    private IWatchBehavior watcher;
    private IAttackBehavior attacker;

    private enum MonsterState { Patrol, Watch, Chase ,Attack}
    private MonsterState state = MonsterState.Patrol;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        lastPosition = transform.position;

        // 전략 할당 (로봇은 일정거리까지 접근후 총알발사)
        detector = new CircleDetector(detectRange, transform);
        chaser = new MaintainDistanceChase(chaseSpeed,stopRange);
        watcher = new PassiveWatch(chaseRange);
        attacker = new ProjectileAttack(objectPool.monsterBulletPool, attackCooldown);
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null) return;

        bool detected = detector.IsPlayerDetected(transform, player);

        switch (state)
        {
            case MonsterState.Patrol:
                Patrol();
                if (detected)
                {
                    state = MonsterState.Watch;
                }
                break;

            case MonsterState.Watch:
                watcher.Watch(transform, player, rb);
                //Patrol();  // ← Watch 중에도 거리는 쌓임
                if (!detected)
                {
                    watcher.ResetWatch();
                    state = MonsterState.Patrol;
                }
                else if (watcher.ShouldChase(transform, player))
                {
                    state = MonsterState.Chase;
                }
                break;

            case MonsterState.Chase:
                float dist = Vector2.Distance(transform.position, player.position);

                if (dist > stopRange)
                {
                    chaser.Chase(transform, player, rb);
                }
                else // 이 부분! stopRange 이내면 Attack 진입
                {
                    rb.velocity = Vector2.zero;
                    state = MonsterState.Attack;
                    
                }
                if (!detected)
                {
                    watcher.ResetWatch();
                    state = MonsterState.Patrol;
                }
                break;

            case MonsterState.Attack:
                if(!detected)
                {
                    watcher.ResetWatch();
                    state = MonsterState.Patrol;
                }
                FlipToPlayer();
                attacker.Attack(transform,player);
                break;
        }
    }

    void Patrol()
    {
        float dir = movingRight ? 1f : -1f;
        rb.velocity = new Vector2(dir * patrolSpeed, rb.velocity.y);


        Vector3 scale = transform.localScale;
        scale.x = movingRight ? 1f : -1f;
        transform.localScale = scale;

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

    void FlipToPlayer()
    {
        if (player.position.x < transform.position.x && transform.localScale.x > 0)
        {
            Vector3 scale = transform.localScale;
            scale.x = -1f;
            transform.localScale = scale;
        }
        else if (player.position.x > transform.position.x && transform.localScale.x < 0)
        {
            Vector3 scale = transform.localScale;
            scale.x = 1f;
            transform.localScale = scale;
        }
    }
    void Flip()
    {
        movingRight = !movingRight;

    }
}
