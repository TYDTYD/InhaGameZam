using UnityEngine;

public class Bullet : MonoBehaviour
{
    ObjectPooling pool;
    Rigidbody2D rb;
    float speed = 40f;
    int damage = 10;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Fire(Vector2 dir, ObjectPooling pooling)
    {
        this.pool = pooling;
        rb.velocity = dir.normalized * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.TryGetComponent(out IHealth health))
        {
            health.TakeDamage(damage);
        }
        rb.velocity = Vector2.zero;
        pool.objectPool.Release(this);
    }
}
