using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossArmAimer : MonoBehaviour
{
    // Start is called before the first frame update
    private Transform player;
    //private Transform armPivot; // 회전 중심(팔 시작점), 보통 자신의 트랜스폼

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    void Update()
    {
        if (player == null) return;

        // 1. 플레이어 방향 계산
        Vector2 dir = player.position - transform.position;
        // 2. 각도 계산 (2D)
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        // 3. z축 회전 적용 (2D)
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}
