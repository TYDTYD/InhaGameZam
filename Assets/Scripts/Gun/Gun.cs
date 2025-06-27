using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
public class Gun : MonoBehaviour
{
    [SerializeField] Bullet bulletPrefab;
    [SerializeField] ObjectPooling bulletPool;
    [SerializeField] Transform bulletSpawnPoint;
    [SerializeField] InputActionReference shootAction;
    [SerializeField] float fireRate = 0.1f;
    private float nextFireTime = 0f;
    WaitForSeconds bulletLifeTime;

    private void Awake()
    {
        bulletLifeTime = new WaitForSeconds(8f);
    }

    void Update()
    {
        if (shootAction.action.IsPressed() && Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }
    }

    void Shoot()
    {
        Bullet bullet = bulletPool.objectPool.Get();
        bullet.transform.position = bulletSpawnPoint.position;
        bullet.transform.rotation = bulletSpawnPoint.rotation;
        bullet.Fire(bulletSpawnPoint.right, bulletPool);

        StartCoroutine(ReturnBullet(bullet));
    }


    IEnumerator ReturnBullet(Bullet bullet)
    {
        yield return bulletLifeTime;
        bulletPool.objectPool.Release(bullet);
    }
}
