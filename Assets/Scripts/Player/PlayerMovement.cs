using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerMovement : MonoBehaviour
{
    Vector2 moveDirection;
    Rigidbody2D rigidBody2D;
    PlayerCollisionCheck collisionCheck;
    PlayerWallCollisionCheck wallCollisionCheck;

    float moveSpeed = 5f;
    float jumpForce = 15f;
    float wallPushForce = 8f;
    float wallJumpCooldown = 0.1f;
    float wallJumpTimer = 0f;
    bool jumped = false;
    bool doubleJumped = false;
    void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        collisionCheck = GetComponentInChildren<PlayerCollisionCheck>();
        wallCollisionCheck = GetComponent<PlayerWallCollisionCheck>();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        Vector2 input = context.ReadValue<Vector2>();
        if (input != null)
        {
            moveDirection = new Vector2(input.x, input.y);
        }
    }

    void OnGroundJump()
    {
        if (collisionCheck.groundCheck)
        {
            jumped = false;
            doubleJumped = false;

            if (!jumped)
            {
                rigidBody2D.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
                jumped = true;
            }
        }
        else if (!jumped)
        {
            rigidBody2D.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            jumped = true;
        }
        else if (!doubleJumped)
        {
            rigidBody2D.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            doubleJumped = true;
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            switch (wallCollisionCheck.wallContacted)
            {
                case ContactInfo.RIGHTWALL:
                    {
                        wallJumpTimer = wallJumpCooldown;
                        rigidBody2D.AddForce(new Vector2(-wallPushForce, jumpForce), ForceMode2D.Impulse);
                        break;
                    }
                case ContactInfo.LEFTWALL:
                    {
                        wallJumpTimer = wallJumpCooldown;
                        rigidBody2D.AddForce(new Vector2(wallPushForce, jumpForce), ForceMode2D.Impulse);
                        break;
                    }
                case ContactInfo.NONE:
                    {
                        OnGroundJump();
                        break;
                    }
            }
        }
    }

    void FixedUpdate()
    {
        if (wallJumpTimer <= 0)
        {
            rigidBody2D.velocity = new Vector2(moveDirection.x * moveSpeed, rigidBody2D.velocity.y);
        }
        else
        {
            wallJumpTimer -= Time.fixedDeltaTime;
        }
    }
}
