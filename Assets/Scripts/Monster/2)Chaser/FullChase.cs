using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FullChase : IChaseBehavior
{
    private float speed;

    public FullChase(float speed)
    {
        this.speed = speed;
    }

    public void Chase(Transform monster, Transform player, Rigidbody2D rb)
    {
        Vector2 direction = (player.position - monster.position).normalized;
        rb.velocity = new Vector2(direction.x * speed, rb.velocity.y);
    }
}