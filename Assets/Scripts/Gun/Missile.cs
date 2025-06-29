using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class Missile : MonoBehaviour
{
    [SerializeField] ParticleSystem trailEffect;
    [SerializeField] GameObject explosionEffect;
    ObjectPool<Missile> objectPool;
    WaitForSeconds missileLifeTime;
    Rigidbody2D rb;
    float speed = 50f;
    int damage = 80;
    float lifeTime = 10f;
    bool isReleased = false;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        missileLifeTime = new WaitForSeconds(lifeTime);
    }

    private void OnEnable()
    {
        trailEffect.Play();
    }

    private void OnDisable()
    {
        trailEffect.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
    }

    public void Init(ObjectPool<Missile> objectPool)
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
        StartCoroutine(ReturnMissile());
    }

    IEnumerator ReturnMissile()
    {
        yield return missileLifeTime;
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
            ParticleManager.PlayParticle(explosionEffect, transform.position, Quaternion.identity);
            CameraManager.Instance.Shake(3.5f, 8f, 1f);
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
            ParticleManager.PlayParticle(explosionEffect, transform.position, Quaternion.identity);
            CameraManager.Instance.Shake(3.5f, 8f, 1f);
            rb.velocity = Vector2.zero;
            if (!isReleased)
            {
                isReleased = true;
                objectPool.Release(this);     
            }
        }
    }
}
