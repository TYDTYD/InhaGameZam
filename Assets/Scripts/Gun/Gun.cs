using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
public class Gun : MonoBehaviour
{
    [SerializeField] ObjectPooling objectPool;
    [SerializeField] Transform bulletSpawnPoint;
    [SerializeField] InputActionReference aimAction;

    float fireRate = 0.1f;
    float nextTimeToShoot;
    float missileRate = 3f;
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

        Vector2 mouseScreenPos = aimAction.action.ReadValue<Vector2>();
        Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(new Vector2(mouseScreenPos.x, mouseScreenPos.y));
        bullet.transform.SetPositionAndRotation(bulletSpawnPoint.transform.position, bulletSpawnPoint.rotation);
        Vector2 dir = mouseWorldPos - (Vector2)bulletSpawnPoint.transform.position;
        bullet.Fire(dir);

        SoundManager.Instance.PlaySound(SoundType.Shoot);

        bullet.Deactivate();

        nextTimeToShoot = Time.fixedTime + fireRate;
    }

    public void MissileShoot()
    {
        Missile missile = objectPool.missilePool.Get();

        if (missile == null)
            return;

        Vector2 mouseScreenPos = aimAction.action.ReadValue<Vector2>();
        Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(new Vector2(mouseScreenPos.x, mouseScreenPos.y));
        missile.transform.SetPositionAndRotation(bulletSpawnPoint.transform.position, bulletSpawnPoint.rotation);
        Vector2 dir = mouseWorldPos - (Vector2)bulletSpawnPoint.transform.position;
        missile.Fire(dir);

        SoundManager.Instance.PlaySound(SoundType.Missile);

        missile.Deactivate();

        nextTimeToMissileShoot = Time.fixedTime + missileRate;
    }
}
