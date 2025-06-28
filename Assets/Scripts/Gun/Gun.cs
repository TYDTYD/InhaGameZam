using UnityEngine;
public class Gun : MonoBehaviour
{
    [SerializeField] ObjectPooling objectPool;
    [SerializeField] Transform bulletSpawnPoint;
    float fireRate = 0.1f;
    float nextTimeToShoot;
    float missileRate = 5f;
    float nextTimeToMissileShoot;
    private void FixedUpdate()
    {
        if (Input.GetMouseButton(0) && Time.fixedTime > nextTimeToShoot && objectPool != null)
        {
            Shoot();
        }

        if(Input.GetMouseButton(1) && Time.fixedTime > nextTimeToMissileShoot && objectPool != null)
        {
            MissileShoot();
        }
    }

    public void Shoot()
    {
        Bullet bullet = objectPool.bulletPool.Get();

        if (bullet == null)
            return;
            

        bullet.transform.SetPositionAndRotation(bulletSpawnPoint.transform.position, bulletSpawnPoint.rotation);
        Vector2 dir = bulletSpawnPoint.transform.position - transform.parent.position;
        bullet.Fire(dir);

        bullet.Deactivate();

        nextTimeToShoot = Time.fixedTime + fireRate;
    }

    public void MissileShoot()
    {
        Missile missile = objectPool.missilePool.Get();

        if (missile == null)
            return;


        missile.transform.SetPositionAndRotation(bulletSpawnPoint.transform.position, bulletSpawnPoint.rotation);
        Vector2 dir = bulletSpawnPoint.transform.position - transform.parent.position;
        missile.Fire(dir);

        missile.Deactivate();

        nextTimeToMissileShoot = Time.fixedTime + missileRate;
    }
}
