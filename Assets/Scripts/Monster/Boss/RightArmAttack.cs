using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightArmAttack : MonoBehaviour
{
<<<<<<< Updated upstream
    [SerializeField] ObjectPooling objectPool;   // ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½Æ® Ç® ï¿½Ò´ï¿½
    [SerializeField] Transform firePoint;        // ï¿½Ñ±ï¿½ ï¿½ï¿½Ä¡ ï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½Æ®
    public int bulletCount = 3;
    public float shootInterval = 3f;           // ï¿½Ñ¾ï¿½ ï¿½ï¿½ï¿½ï¿½
=======
    [SerializeField] ObjectPooling objectPool;   // ¿ÀºêÁ§Æ® Ç® ÇÒ´ç
    [SerializeField] Transform firePoint;        // ÃÑ±¸ À§Ä¡ ºó ¿ÀºêÁ§Æ®
    public int bulletCount = 3;
    public float shootInterval = 3f;           // ÃÑ¾Ë °£°Ý
>>>>>>> Stashed changes


    [SerializeField] Transform player;

<<<<<<< Updated upstream

    void Update()
    {
        if (player != null)
        {
            Debug.Log(player.position);
            Vector2 dir = player.position - transform.position;

            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg + 180f;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }

    }


=======
>>>>>>> Stashed changes
    public void FireBullets()
    {
        StartCoroutine(FireCoroutine());
    }

    IEnumerator FireCoroutine()
    {
        for (int i = 0; i < bulletCount; i++)
        {
            BossBullet2 bullet = objectPool.bossBullet2Pool.Get();
            bullet.transform.position = firePoint.position;
            bullet.transform.rotation = firePoint.rotation;
<<<<<<< Updated upstream
            // ï¿½Ã·ï¿½ï¿½Ì¾ï¿½ ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½ï¿½
            Vector2 dir = (player.position - firePoint.position).normalized;
            bullet.Fire(dir);  // ï¿½ï¿½ ï¿½Ã·ï¿½ï¿½Ì¾ï¿½ ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ ï¿½ß»ï¿½
=======
            // ÇÃ·¹ÀÌ¾î ¹æÇâ °è»ê
            Vector2 dir = (player.position - firePoint.position).normalized;
            bullet.Fire(dir);  // ¡ç ÇÃ·¹ÀÌ¾î ÂÊÀ¸·Î ¹ß»ç
>>>>>>> Stashed changes
            yield return new WaitForSeconds(shootInterval);
        }
    }
}
