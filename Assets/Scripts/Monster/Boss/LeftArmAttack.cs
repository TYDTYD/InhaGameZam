using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftArmAttack : MonoBehaviour
{
    [SerializeField] ObjectPooling objectPool;   // 오브젝트 풀 할당
    [SerializeField] Transform firePoint;        // 총구 위치 빈 오브젝트
    [SerializeField] Transform player;        // 총구 위치 빈 오브젝트
    public int bulletCount = 20;
    public float shootInterval = 1f;           // 총알 간격



    void Update()
    {
        if (player != null)
        {
            // 1. 팔(LeftArm)이 플레이어를 향하게 회전
            Debug.Log(player.position);
            Vector2 dir = player.position - transform.position;
            
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg + 180f;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    
    }

    public void FireBullets()
    {
        StartCoroutine(FireCoroutine());
    }

    IEnumerator FireCoroutine()
    {
        for (int i = 0; i < bulletCount; i++)
        {
            BossBullet bullet = objectPool.bossBulletPool.Get();
            bullet.transform.position = firePoint.position;
            bullet.transform.rotation = firePoint.rotation;
            
            // 플레이어 방향 계산
            Vector2 dir = (player.position - firePoint.position).normalized;
            bullet.Fire(dir);  // ← 플레이어 쪽으로 발사

            yield return new WaitForSeconds(shootInterval);
        }
    }
}
