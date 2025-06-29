using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    Animator animator;
    PlayerMovement playerMovement;

    int Idle;
    int Dash;
    int Walk;
    int Jump;
    MoveState previousState;

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        playerMovement = GetComponent<PlayerMovement>();

        Idle = Animator.StringToHash("Idle");
        Dash = Animator.StringToHash("Dashing");
        Walk = Animator.StringToHash("Walking");
        Jump = Animator.StringToHash("Jumping");
        
        previousState = MoveState.NONE;
    }

    void ResetAllTriggers()
    {
        animator.ResetTrigger(Idle);
        animator.ResetTrigger(Dash);
        animator.ResetTrigger(Walk);
        animator.ResetTrigger(Jump);
    }

    void Update()
    {
        MoveState currentState = playerMovement.moveState;

        if (currentState == previousState) 
            return; // 상태가 바뀌지 않았으면 무시

        ResetAllTriggers();

        switch (playerMovement.moveState)
        {
            case MoveState.NONE:
                {
                    animator.SetTrigger(Idle);
                    break;
                }
            case MoveState.MOVE:
                {
                    animator.SetTrigger(Walk);
                    break;
                }
            case MoveState.DASH:
                {
                    animator.SetTrigger(Dash);
                    break;
                }
            case MoveState.JUMP:
                {
                    animator.SetTrigger(Jump);
                    break;
                }
        }

        previousState = currentState;
    }
}
