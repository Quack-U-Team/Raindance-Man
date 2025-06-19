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

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {
            auraEnemyScript.target = collision.transform;
            playerScript.ansia = true;
            
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {   
        
        if (collision.gameObject.CompareTag("Player"))
        {
            
            Debug.Log("AuraDamage Stay");
            auraEnemyScript.gotPlrTransform();
            playerScript.mentalPointsRemove(auraDamage );
            
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("AuraDamage Exit");
            auraEnemyScript.gotPlrTransform();
            auraEnemyScript.target = null;
        }
    }
}
