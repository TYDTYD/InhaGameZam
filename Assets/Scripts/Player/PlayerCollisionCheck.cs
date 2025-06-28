using UnityEngine;

public class PlayerCollisionCheck : MonoBehaviour
{
    bool groundCheck = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {            
            groundCheck = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            groundCheck = false;
        }
    }

    public bool GroundCheck => groundCheck;
}
