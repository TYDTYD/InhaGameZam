using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalRangeDetector : IPlayerDetector
{
    private float rangeX;
    private Transform monsterTransform;
    public HorizontalRangeDetector(float rangeX, Transform monsterTransform)
    {
        this.rangeX = rangeX;
        this.monsterTransform = monsterTransform;
    }

    public bool IsPlayerDetected(Transform monster, Transform player)
    {
        float dx = player.position.x - monster.position.x;
        bool playerInFront =
            (dx > 0 && monsterTransform.localScale.x > 0) || // 오른쪽 바라보고 오른쪽에 있음
            (dx < 0 && monsterTransform.localScale.x < 0);   // 왼쪽 바라보고 왼쪽에 있음
        return Mathf.Abs(dx) < rangeX && playerInFront;
    }
}