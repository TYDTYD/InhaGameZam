using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class Bullet : MonoBehaviour
{
    [SerializeField] ParticleSystem trailEffect;
    ObjectPool<Bullet> objectPool;
    WaitForSeconds bulletLifeTime;
    Rigidbody2D rb;
    float speed = 40f;
    int damage = 10;
    float lifeTime = 8f;
    bool isReleased = false;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        bulletLifeTime = new WaitForSeconds(lifeTime);
    }

    private void OnEnable()
    {
        trailEffect.Play();
    }

    private void OnDisable()
    {
        trailEffect.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
    }

    public void Init(ObjectPool<Bullet> objectPool)
    {
        this.objectPool = objectPool;
    }

    public void Fire(Vector2 dir)
    {
        isReleased = false;
        rb.velocity = dir.normalized * speed;
    }

    public void Deactivate()
    {
        StartCoroutine(ReturnBullet());
    }

    IEnumerator ReturnBullet()
    {
        yield return bulletLifeTime;
        rb.velocity = Vector2.zero;
        objectPool.Release(this);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Bullet"))
        {
            return;
        }
        if (collision.gameObject.TryGetComponent(out IHealth health))
        {
            health.TakeDamage(damage);
            rb.velocity = Vector2.zero;
            if (!isReleased)
            {
                isReleased = true;
                objectPool.Release(this);                
            }
        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            rb.velocity = Vector2.zero;
            if (!isReleased)
            {
                isReleased = true;
                objectPool.Release(this);
            }
        }
    }
}
