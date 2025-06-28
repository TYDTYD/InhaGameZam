using UnityEngine;
public enum ContactInfo
{
    LEFTWALL,
    RIGHTWALL,
    NONE,
}

public class PlayerWallCollisionCheck : MonoBehaviour
{
    [HideInInspector]
    public ContactInfo wallContacted = ContactInfo.NONE;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        foreach (ContactPoint2D contact in collision.contacts)
        {
            // 정확한 벽 방향 감지를 위해 x 방향 법선값을 비교
            if (Mathf.Abs(contact.normal.x) > Mathf.Abs(contact.normal.y))
            {
                if (contact.normal.x > 0.5f)
                {
                    wallContacted = ContactInfo.LEFTWALL; // 왼쪽 벽에 닿았을 때 (벽이 왼쪽에 있음)
                    return;
                }
                else if (contact.normal.x < -0.5f)
                {
                    wallContacted = ContactInfo.RIGHTWALL; // 오른쪽 벽에 닿았을 때 (벽이 오른쪽에 있음)
                    return;
                }
            }
        }

        wallContacted = ContactInfo.NONE;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (wallContacted != ContactInfo.NONE)
        {
            wallContacted = ContactInfo.NONE;
        }
    }
}
