using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class MonsterBullet : MonoBehaviour
{
    //[SerializeField] ParticleSystem trailEffect;
    ObjectPool<MonsterBullet> objectPool;
    WaitForSeconds bulletLifeTime;
    Rigidbody2D rb;
    float speed = 10f; // 몬스터 총알은 느려도 됨
    int damage = 10;
    float lifeTime = 6f;
    bool isReleased = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        bulletLifeTime = new WaitForSeconds(lifeTime);
    }

    private void OnEnable()
    {
       // trailEffect?.Play();
    }

    private void OnDisable()
    {
        //trailEffect?.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
    }

    public void Init(ObjectPool<MonsterBullet> objectPool)
    {
        this.objectPool = objectPool;
    }

    public void Fire(Vector2 dir)
    {
        isReleased = false;
        rb.velocity = dir.normalized * speed;
        StartCoroutine(ReturnBullet());
    }

    public void Deactivate()
    {
        StopAllCoroutines();
        rb.velocity = Vector2.zero;
        objectPool.Release(this);
    }

    IEnumerator ReturnBullet()
    {
        yield return bulletLifeTime;
        rb.velocity = Vector2.zero;
        objectPool.Release(this);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 플레이어만 맞추게 처리
        if (collision.CompareTag("Player"))
        {
            // 데미지 처리
            if (collision.TryGetComponent(out IHealth health))
            {
                health.TakeDamage(damage);
            }

            rb.velocity = Vector2.zero;
            if (!isReleased)
            {
                isReleased = true;
                objectPool.Release(this);
            }
        }
        // 벽, 바닥 등 기타 모든 물리 오브젝트에도 반응
        else if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            rb.velocity = Vector2.zero;
            if (!isReleased)
            {
                isReleased = true;
                objectPool.Release(this);
            }
        }
        // 그 외엔 무시 (적끼리 충돌 X, 다른 총알 등)
    }
}