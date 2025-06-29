using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Urchin : MonoBehaviour
{
    [Header("수치 설정")]
    public float patrolSpeed = 1f;
    public float maxPatrolDistance = 7f;
    public float detectRange = 10f;
    public float chaseSpeed = 3f;
    public float chaseRange = 10f;
    public int attackDamage = 2;
    public float attackCooldown = 1f;

    public LayerMask groundLayer;


    private Rigidbody2D rb;
    private Transform player;
    private Vector2 lastPosition;
    
    private bool movingRight = true;

    private IPlayerDetector detector;
    private IChaseBehavior chaser;
    private IWatchBehavior watcher;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        lastPosition = transform.position;

        // 전략 할당 (슬라임은 단순 추적 + 몸통박치기)
        detector = new CircleDetector(detectRange, transform);
        chaser = new FullChase(chaseSpeed);
        watcher = new PassiveWatch(chaseRange);
    }
    void Update()
    {
        if (player == null) return;

        bool detected = detector.IsPlayerDetected(transform, player);
        if (detected)
        {
            RotateToPlayer(); // 몸 전체 회전
            FlipXToPlayer();  // 얼굴(눈) 자연스럽게 뒤집기
            
            
            ChasePlayer();    // 계속 추적
            transform.rotation = Quaternion.identity;
        }
        else
        {
            rb.velocity = Vector2.zero; // 감지 안 되면 멈춤
        }
    }

    void ChasePlayer()
    {
        Vector2 dir = (player.position - transform.position).normalized;
        rb.velocity = dir * chaseSpeed;
    }
    void RotateToPlayer()
    {
        Vector2 dir = player.position - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
    void FlipXToPlayer()
    {
        if (player.position.x < transform.position.x)
            transform.localScale = new Vector3(1, 1, 1);   // 왼쪽
        else
            transform.localScale = new Vector3(-1, 1, 1);  // 오른쪽
    }
    void Flip()
    {
        movingRight = !movingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}