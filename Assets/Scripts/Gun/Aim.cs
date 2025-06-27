using UnityEngine;
using UnityEngine.InputSystem;

public class Aim : MonoBehaviour
{
    [SerializeField] InputActionReference aimAction; // Point or Look 액션
    float offsetDistance;
    Camera mainCam;

    private void Awake()
    {
        mainCam = Camera.main;
        offsetDistance = Vector2.Distance(transform.position, transform.parent.position);
    }

    void Update()
    {
        Vector2 mouseScreenPos = aimAction.action.ReadValue<Vector2>();
        Vector3 mouseWorldPos = mainCam.ScreenToWorldPoint(mouseScreenPos);
        Vector2 direction = mouseWorldPos - transform.parent.position;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle);

        Vector2 offset = direction.normalized * offsetDistance;
        transform.position = transform.parent.position + new Vector3(offset.x, offset.y, 0f);

        if (direction.x < 0)
            transform.localScale = new Vector3(1, -transform.localScale.y, 1); // y축 플립
        else
            transform.localScale = new Vector3(1, transform.localScale.y, 1);

    }
}
