using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveWatch : IWatchBehavior
{
    private float chaseRange;
    private float watchStartTime = -1f;
    private bool isWatching = false;
    public PassiveWatch(float chaseRange)
    {
        this.chaseRange = chaseRange;
    }

    public void Watch(Transform monster, Transform player, Rigidbody2D rb)
    {
        if (!isWatching)
        {
            watchStartTime = Time.time;
            isWatching = true;
        }
        // 바라보기만 하고 안 움직임
        if (player.position.x < monster.position.x && monster.localScale.x > 0)
            monster.localScale = new Vector3(-1, 1, 1);
        else if (player.position.x > monster.position.x && monster.localScale.x < 0)
            monster.localScale = new Vector3(1, 1, 1);


        rb.velocity = Vector2.zero;
    }

    public bool ShouldChase(Transform monster, Transform player)
    {
        return isWatching && Time.time - watchStartTime >= 1f && Mathf.Abs(player.position.x - monster.position.x) < chaseRange;
    }

    public void ResetWatch()
    {
        isWatching = false;
        watchStartTime = -1f;
    }
}