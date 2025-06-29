using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftArmAttack : MonoBehaviour
{
    [SerializeField] ObjectPooling objectPool;   // ������Ʈ Ǯ �Ҵ�
    [SerializeField] Transform firePoint;        // �ѱ� ��ġ �� ������Ʈ
    [SerializeField] Transform player;        // �ѱ� ��ġ �� ������Ʈ
    public int bulletCount = 20;
    public float shootInterval = 1f;           // �Ѿ� ����



    void Update()
    {
        if (player != null)
        {
            // 1. ��(LeftArm)�� �÷��̾ ���ϰ� ȸ��
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
            
            // �÷��̾� ���� ���
            Vector2 dir = (player.position - firePoint.position).normalized;
            bullet.Fire(dir);  // �� �÷��̾� ������ �߻�

            yield return new WaitForSeconds(shootInterval);
        }
    }
}
