using UnityEngine;

public class CursorFollower : MonoBehaviour
{
    RectTransform cursorImage;
    void Start()
    {
        cursorImage = GetComponent<RectTransform>();
        Cursor.visible = false;
    }

    private void OnEnable()
    {
        if (UpdateManager.Instance != null)
            UpdateManager.Instance.SubscribeUpdate(UpdateMethod);
    }

    void UpdateMethod()
    {
        Vector2 mousePosition = Input.mousePosition;
        cursorImage.position = mousePosition;
    }

    private void OnDisable()
    {
        if (UpdateManager.Instance != null)
            UpdateManager.Instance.UnsubscribeUpdate(UpdateMethod);
    }
}