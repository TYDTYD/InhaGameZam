using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpperHalfCircleDetector : IPlayerDetector
{
    private float radius;

    public UpperHalfCircleDetector(float radius)
    {
        this.radius = radius;
    }

    public bool IsPlayerDetected(Transform monster, Transform player)
    {
        Vector2 dir = player.position - monster.position;

        // 전체 원 안에 들어오고
        if (dir.magnitude < radius)
        {
            // 위쪽(상반구)에 있을 때만 감지
            return dir.y >= 0;
        }

        return false;
    }
}