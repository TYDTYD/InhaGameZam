using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerMovement : MonoBehaviour
{
    Vector2 moveDirection;
    Rigidbody2D rigidBody2D;

    float moveSpeed = 5f;
    float jumpForce = 15f;
    void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        Vector2 input = context.ReadValue<Vector2>();
        if (input != null)
        {
            moveDirection = new Vector2(input.x, input.y);
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            rigidBody2D.AddForce(new Vector2(0f, jumpForce),ForceMode2D.Impulse);
        }
    }

    private void FixedUpdate()
    {
        rigidBody2D.velocity = new Vector2(moveDirection.x * moveSpeed, rigidBody2D.velocity.y);
    }
}
