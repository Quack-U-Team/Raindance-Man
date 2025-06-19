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

    private void Update()
    {
        CheckPlayerState();
    }

    void CheckPlayerState()
    {
        curState = playerMovement.playerState;

       

    }
}
