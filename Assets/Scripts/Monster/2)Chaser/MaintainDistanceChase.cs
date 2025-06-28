using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaintainDistanceChase : IChaseBehavior
{
    private float speed;
    private float stopDistance;

    public MaintainDistanceChase(float speed, float stopDistance)
    {
        this.speed = speed;
        this.stopDistance = stopDistance;
    }

    public void Chase(Transform monster, Transform player, Rigidbody2D rb)
    {
        float distance = Vector2.Distance(monster.position, player.position);

        if (distance > stopDistance)
        {
            Vector2 direction = (player.position - monster.position).normalized;
            rb.velocity = new Vector2(direction.x * speed, rb.velocity.y);
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }
}