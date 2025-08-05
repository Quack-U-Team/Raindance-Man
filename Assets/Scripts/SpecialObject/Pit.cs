using UnityEngine;

public class Pit : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        print("collided with something");
        PlayerMovement pm;
        if (collision.CompareTag("Player"))
        {
            if(collision.gameObject.TryGetComponent<PlayerMovement>(out pm))
            {
                if(pm.playerState != PlayerMovement.PlayerState.Dashing)
                {
                    print("player fell into a pit");
                    pm.deathAnim();
                }
            }
            else
            {
                Debug.LogWarning("Pit.cs - couldn't get PlayerMovement from collided object");
            }
        }
    }
    

        
    
}
