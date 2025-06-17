using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private Animator playerAnimator;
    private PlayerMovement.PlayerState curState;

    private void Awake()
    {
        playerMovement = this.GetComponent<PlayerMovement>();
        playerAnimator = this.GetComponentInChildren<Animator>();
    }

    private void Start()
    {
        playerAnimator.SetTrigger("spawn");
    }

    private void Update()
    {
        CheckPlayerState();
    }

    void CheckPlayerState()
    {
        curState = playerMovement.playerState;

        if(curState == PlayerMovement.PlayerState.Idle)
        {
            playerAnimator.SetTrigger("idle");
        }

        if (curState == PlayerMovement.PlayerState.Dashing)
        {
            playerAnimator.SetTrigger("roll");
        }

        if (curState == PlayerMovement.PlayerState.Shooting)
        {
            playerAnimator.SetTrigger("shoot");
        }

    }
}
