using UnityEngine;

public class Pit : MonoBehaviour
{

    private void OnCollisionEnter2D(Collision2D collision)
    {
        print("collided");
        if(collision.gameObject.GetComponent<PlayerMovement>() != null)
        {
            PlayerMovement pm = collision.gameObject.GetComponent<PlayerMovement>();
            print("got PlayerMovement");
            if(pm.playerState != PlayerMovement.PlayerState.Dashing)
            {
                print("pm.deathAnim()");
                pm.deathAnim();
            }
        }
    }




}
