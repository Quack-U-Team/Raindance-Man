using UnityEngine;

public class AuraDamage : MonoBehaviour
{
    public float auraDamage = 1.0f;
    public PlayerMovement playerScript;
    public auraEnemy auraEnemyScript;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    

    private void OnTriggerStay2D(Collider2D collision)
    {   
        
        if (collision.gameObject.CompareTag("Player"))
        {
            
            Debug.Log("AuraDamage Stay");
            auraEnemyScript.gotPlrTransform();
            playerScript.mentalPointsRemove(auraDamage );
            playerScript.ansia = true;

        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (auraEnemyScript != null)
            {
                Debug.Log("AuraDamage Exit");
                auraEnemyScript.RefreshAI();
                auraEnemyScript.target = null;
            }
        } 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {
            if (auraEnemyScript != null)
            {
                Debug.Log("AuraDamage Exit");
                auraEnemyScript.RefreshAI();
                auraEnemyScript.target = collision.transform;
            }
            playerScript = collision.gameObject.GetComponent<PlayerMovement>();
        }
    }

}
