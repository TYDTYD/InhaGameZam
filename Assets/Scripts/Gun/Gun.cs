using System.Collections;
using UnityEngine;
public class Gun : MonoBehaviour
{
    [SerializeField] Bullet bulletPrefab;
    [SerializeField] ObjectPooling bulletPool;
    [SerializeField] Transform bulletSpawnPoint;
    float fireRate = 0.1f;
    float nextTimeToShoot;
    private void FixedUpdate()
    {
        if (Input.GetMouseButton(0) && Time.fixedTime > nextTimeToShoot && bulletPool != null)
        {
            Shoot();
        }
    }

    public void Shoot()
    {
        Bullet bullet = bulletPool.objectPool.Get();

        if (bullet == null)
            return;
            

        bullet.transform.SetPositionAndRotation(bulletSpawnPoint.transform.position, bulletSpawnPoint.rotation);
        Vector2 dir = bulletSpawnPoint.transform.position - transform.parent.position;
        bullet.Fire(dir);

        bullet.Deactivate();

        nextTimeToShoot = Time.fixedTime + fireRate;
    }
}
