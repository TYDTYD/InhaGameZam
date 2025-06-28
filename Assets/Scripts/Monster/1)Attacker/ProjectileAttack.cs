using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileAttack : IAttackBehavior
{
    private GameObject projectilePrefab;
    private float cooldown;
    private float lastAttackTime = -999f;

    public ProjectileAttack(GameObject prefab, float cooldown)
    {
        this.projectilePrefab = prefab;
        this.cooldown = cooldown;
    }

    public void Attack(Transform monster, Transform player)
    {
        if (Time.time - lastAttackTime < cooldown)
            return;

        Vector2 direction = (player.position - monster.position).normalized;

        Debug.Log("발사체 공격!");
        GameObject proj = GameObject.Instantiate(projectilePrefab, monster.position, Quaternion.identity);
        Rigidbody2D rb = proj.GetComponent<Rigidbody2D>();
        rb.velocity = direction * 5f;

        lastAttackTime = Time.time;
    }
}