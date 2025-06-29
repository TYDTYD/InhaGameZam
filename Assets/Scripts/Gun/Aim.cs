using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class Aim : MonoBehaviour
{
    [SerializeField] InputActionReference aimAction; // Point or Look 액션
    float offsetDistance;
    Camera mainCam;
    float offsetZ;

    private void Awake()
    {
        mainCam = Camera.main;
        offsetDistance = Vector2.Distance(transform.position, transform.root.position);
        offsetZ = transform.localEulerAngles.z;
    }

    void Update()
    {
        Vector2 mouseScreenPos = aimAction.action.ReadValue<Vector2>();
        Vector3 mouseWorldPos = mainCam.ScreenToWorldPoint(mouseScreenPos);
        Vector2 direction = mouseWorldPos - transform.transform.position;

        float offsetX = mouseWorldPos.x - transform.position.x;

        Vector3 scale = transform.localScale;

        if (offsetX > 0)
        {
            scale.x = Mathf.Abs(scale.x); // 오른쪽 → 양수
            scale.y= Mathf.Abs(scale.y);
        }            
        else if (offsetX < 0)
        {
            scale.x = -Mathf.Abs(scale.x); // 왼쪽 → 음수
            scale.y = -Mathf.Abs(scale.y);
        }
        transform.localScale = scale;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle + offsetZ);

        Vector2 offset = direction.normalized * offsetDistance;
        transform.position = transform.root.position + new Vector3(offset.x, offset.y, 0f);
    }
}
