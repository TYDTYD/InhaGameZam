using UnityEngine;

public class CursorFollower : MonoBehaviour
{
    RectTransform cursorImage;
    void Awake()
    {
        cursorImage = GetComponent<RectTransform>();
        Cursor.visible = false;
    }


    private void LateUpdate()
    {
        Vector2 mousePosition = Input.mousePosition;
        cursorImage.position = mousePosition;
    }

    //private void OnEnable()
    //{
    //    if (UpdateManager.Instance != null)
    //        UpdateManager.Instance.SubscribeLateUpdate(LateUpdateMethod);
    //}

    //void LateUpdateMethod()
    //{
    //    Vector2 mousePosition = Input.mousePosition;
    //    cursorImage.position = mousePosition;
    //}


    //private void OnDisable()
    //{
    //    if (UpdateManager.Instance != null)
    //        UpdateManager.Instance.UnsubscribeLateUpdate(LateUpdateMethod);
    //}
}