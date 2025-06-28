using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpperHalfCircleDetector : IPlayerDetector
{
    private float radius;
    private Transform Transform;
    public UpperHalfCircleDetector(float radius, Transform transform)
    {
        this.radius = radius;
        Transform = transform;
    }

    public bool IsPlayerDetected(Transform monster, Transform player)
    {
        Vector2 dir = player.position - monster.position;

        // 전체 원 안에 들어오고
        if (dir.y < 0) return false; // 아래에 있으면 무시
        if (dir.magnitude > radius) return false;


        return false;
    }
}