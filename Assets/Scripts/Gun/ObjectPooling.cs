using UnityEngine;
using UnityEngine.Pool;


// 오브젝트 풀링을 위한 스크립트
public class ObjectPooling : MonoBehaviour
{
    [SerializeField] Bullet bulletPrefab;
    public ObjectPool<Bullet> objectPool;
    
    void Awake()
    {
        objectPool = new ObjectPool<Bullet>(
            CreateBullet,
            OnGetBullet,
            OnReleaseBullet,
            OnDestroyBullet,
            false, // Make sure to set this to false if you want to control the pool size manually
            50, // Initial size of the pool
            100 // Maximum size of the pool
        );
    }

    Bullet CreateBullet()
    {
        GameObject bulletObject = Instantiate(bulletPrefab.gameObject);
        Bullet bullet = bulletObject.GetComponent<Bullet>();
        bullet.transform.SetParent(transform);
        return bullet;
    }

    void OnGetBullet(Bullet bullet)
    {
        bullet.gameObject.SetActive(true);
        // Initialize bullet properties if needed
    }
    void OnReleaseBullet(Bullet bullet)
    {
        bullet.gameObject.SetActive(false);
        // Reset bullet properties if needed
    }
    void OnDestroyBullet(Bullet bullet)
    {
        Destroy(bullet.gameObject);
    }
}
