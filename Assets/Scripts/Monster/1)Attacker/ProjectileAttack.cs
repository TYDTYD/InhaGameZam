using UnityEngine;
using UnityEngine.Pool;

public class ProjectileAttack : IAttackBehavior
{
    private ObjectPool<MonsterBullet> pool;
    private float cooldown;
    private float lastAttackTime = -999f;

    public ProjectileAttack(ObjectPool<MonsterBullet> pool, float cooldown)
    {
        this.pool = pool;
        this.cooldown = cooldown;
    }

    public void Attack(Transform monster, Transform player)
    {
        if (Time.time - lastAttackTime < cooldown)
            return;

        Vector2 direction = (player.position - monster.position).normalized;

        MonsterBullet bullet = pool.Get();
        bullet.transform.position = monster.position;
        bullet.Fire(direction); // 방향 넘기기

        lastAttackTime = Time.time;
    }
}