using UnityEngine;

public class AuraDamage : MonoBehaviour
{
    public float auraDamage = 1.0f;
    public PlayerMovement playerScript;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {
           
            
            
                playerScript.ansia = true;
            
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {   
        
        if (collision.gameObject.CompareTag("Player"))
        {
            
            Debug.Log("AuraDamage Stay");
           
                playerScript.mentalPointsRemove(auraDamage );
            
        }
    }
}
