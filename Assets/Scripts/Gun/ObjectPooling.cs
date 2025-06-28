using UnityEngine;
using UnityEngine.Pool;


// 오브젝트 풀링을 위한 스크립트
public class ObjectPooling : MonoBehaviour
{
    [SerializeField] Bullet bulletPrefab;
    public ObjectPool<Bullet> objectPool;
    int defaultCapacity = 40;
    int maxSize = 100;

    void Awake()
    {
        objectPool = new ObjectPool<Bullet>(
            CreateBullet,
            OnGetBullet,
            OnReleaseBullet,
            OnDestroyBullet,
            true, // Make sure to set this to false if you want to control the pool size manually
            defaultCapacity, // Initial size of the pool
            maxSize // Maximum size of the pool
        );
    }

    Bullet CreateBullet()
    {
        Bullet bullet = Instantiate(bulletPrefab);
        bullet.Init(objectPool);
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
}
