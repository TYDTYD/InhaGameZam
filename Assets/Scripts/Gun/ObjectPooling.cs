using UnityEngine;
using UnityEngine.Pool;


// 오브젝트 풀링을 위한 스크립트
public class ObjectPooling : MonoBehaviour
{
    [SerializeField] Bullet bulletPrefab;
    [SerializeField] Missile missilePrefab;
    public ObjectPool<Bullet> bulletPool;
    public ObjectPool<Missile> missilePool;
    int defaultCapacity = 40;
    int maxSize = 100;

    void Awake()
    {
        bulletPool = new ObjectPool<Bullet>(
            CreateBullet,
            OnGetBullet,
            OnReleaseBullet,
            OnDestroyBullet,
            true, // Make sure to set this to false if you want to control the pool size manually
            defaultCapacity, // Initial size of the pool
            maxSize // Maximum size of the pool
        );

        missilePool = new ObjectPool<Missile>(
            CreateMissile,
            OnGetMissile,
            OnReleaseMissile,
            OnDestroyMissile,
            true,
            defaultCapacity,
            maxSize
            );
    }

    Bullet CreateBullet()
    {
        Bullet bullet = Instantiate(bulletPrefab);
        bullet.Init(bulletPool);
        bullet.transform.SetParent(transform);
        return bullet;
    }

    void OnGetBullet(Bullet bullet)
    {
        if (bullet != null)
        {
            bullet.gameObject.SetActive(true);
        }
    }
    void OnReleaseBullet(Bullet bullet)
    {
        if (bullet!=null)
        {
            bullet.gameObject.SetActive(false);
        }
    }
    void OnDestroyBullet(Bullet bullet)
    {
        Destroy(bullet.gameObject);
    }

    Missile CreateMissile()
    {
        Missile missile = Instantiate(missilePrefab);
        missile.Init(missilePool);
        missile.transform.SetParent(transform);
        return missile;
    }

    void OnGetMissile(Missile missile)
    {
        if (missile != null)
        {
            missile.gameObject.SetActive(true);
        }
    }
    void OnReleaseMissile(Missile missile)
    {
        if (missile != null)
        {
            missile.gameObject.SetActive(false);
        }
    }
    void OnDestroyMissile(Missile missile)
    {
        Destroy(missile.gameObject);
    }
}
