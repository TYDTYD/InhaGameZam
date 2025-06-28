using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleDetector : IPlayerDetector
{
    private float radius;

    public CircleDetector(float radius)
    {
        this.radius = radius;
    }

    public bool IsPlayerDetected(Transform monster, Transform player)
    {
        return Vector2.Distance(monster.position, player.position) < radius;
    }
}