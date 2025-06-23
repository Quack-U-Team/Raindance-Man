using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Candle : MonoBehaviour
{
    PlayerMovement playerMovement = null;
    public float removeDebuffTimer = 2f;

    void RemoveDebuff()
    {
        playerMovement.ansia = false;
        playerMovement.depressione = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            print("entered heal area");
            if (collision.GetComponent<PlayerMovement>() != null) 
            { 
                playerMovement = collision.GetComponent<PlayerMovement>();
                Invoke("RemoveDebuff", removeDebuffTimer);
            }
        }
    }
}