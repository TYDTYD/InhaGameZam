using UnityEngine;
using UnityEngine.Pool;


// 오브젝트 풀링을 위한 스크립트
public class ObjectPooling : MonoBehaviour
{

    //기본 설정 
    int defaultCapacity = 40;   //기본 큐 사이즈
    int maxSize = 100;          //최대 큐 사이즈
    
    //플레이어용 
    [SerializeField] Bullet bulletPrefab;
    [SerializeField] Missile missilePrefab;
    public ObjectPool<Bullet> bulletPool;
    public ObjectPool<Missile> missilePool;
    

    //몬스터용
    [SerializeField] MonsterBullet monsterBulletPrefab;
    public ObjectPool<MonsterBullet> monsterBulletPool;

    //보스용
    [SerializeField] BossBullet bossBulletPrefab;
    public ObjectPool<BossBullet> bossBulletPool;
    [SerializeField] BossBullet2 bossBullet2Prefab;
    public ObjectPool<BossBullet2> bossBullet2Pool;

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
        monsterBulletPool = new ObjectPool<MonsterBullet>(
            CreateMonsterBullet,
            OnGetMonsterBullet,
            OnReleaseMonsterBullet,
            OnDestroyMonsterBullet,
            true, 
            defaultCapacity, 
            maxSize
            );
        bossBulletPool = new ObjectPool<BossBullet>(
            CreateBossBullet,
            OnGetBossBullet,
            OnReleaseBossBullet,
            OnDestroyBossBullet,
            true,
            defaultCapacity,
            maxSize
        );
        bossBullet2Pool = new ObjectPool<BossBullet2>(
            CreateBossBullet2,
            OnGetBossBullet2,
            OnReleaseBossBullet2,
            OnDestroyBossBullet2,
            true,
            defaultCapacity,
            maxSize
        );
    }


    //플레이어 총알(좌클릭)
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


    //플레이어 미사일(우클릭)
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


    //몬스터 총알
    MonsterBullet CreateMonsterBullet()
    {
        MonsterBullet mbullet = Instantiate(monsterBulletPrefab);
        mbullet.Init(monsterBulletPool);
        mbullet.transform.SetParent(transform);
        return mbullet;
    }
    void OnGetMonsterBullet(MonsterBullet mbullet)
    {
        if (mbullet != null)
        {
            mbullet.gameObject.SetActive(true);
        }
    }
    void OnReleaseMonsterBullet(MonsterBullet mbullet)
    {
        if (mbullet != null)
        {
            mbullet.gameObject.SetActive(false);
        }
    }
    void OnDestroyMonsterBullet(MonsterBullet mbullet)
    {
        Destroy(mbullet.gameObject);
    }

    //보스 총알
    BossBullet CreateBossBullet()
    {
        BossBullet bossbullet = Instantiate(bossBulletPrefab);
        bossbullet.Init(bossBulletPool);
        bossbullet.transform.SetParent(transform);
        return bossbullet;
    }
    void OnGetBossBullet(BossBullet bossbullet)
    {
        if (bossbullet != null)
        {
            bossbullet.gameObject.SetActive(true);
        }
    }
    void OnReleaseBossBullet(BossBullet bossbullet)
    {
        if (bossbullet != null)
        {
            bossbullet.gameObject.SetActive(false);
        }
    }
    void OnDestroyBossBullet(BossBullet bossbullet)
    {
        Destroy(bossbullet.gameObject);
    }

    //보스 총알2
    BossBullet2 CreateBossBullet2()
    {
        BossBullet2 bossbullet2 = Instantiate(bossBullet2Prefab);
        bossbullet2.Init(bossBullet2Pool);
        bossbullet2.transform.SetParent(transform);
        return bossbullet2;
    }
    void OnGetBossBullet2(BossBullet2 bossbullet)
    {
        if (bossbullet != null)
        {
            bossbullet.gameObject.SetActive(true);
        }
    }
    void OnReleaseBossBullet2(BossBullet2 bossbullet2)
    {
        if (bossbullet2 != null)
        {
            bossbullet2.gameObject.SetActive(false);
        }
    }
    void OnDestroyBossBullet2(BossBullet2 bossbullet2)
    {
        Destroy(bossbullet2.gameObject);
    }
}
