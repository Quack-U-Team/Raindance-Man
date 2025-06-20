using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class EnemyPlayerVision : MonoBehaviour
{
    public auraEnemy auraEnemyScript;
    public chainEnemy chainEnemyScript;
    public Enemy enemyScript;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
            if (enemyScript != null)
            {
                Debug.Log("AuraDamage Exit");
                enemyScript.RefreshAI();
                enemyScript.target = null;
            }
            if (chainEnemyScript != null)
            {
                Debug.Log("AuraDamage Exit");
                chainEnemyScript.RefreshAI();
                chainEnemyScript.target = null;
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
            if (enemyScript != null)
            {
                Debug.Log("AuraDamage Exit");
                enemyScript.RefreshAI();
                enemyScript.target = collision.transform;
            }
            if (chainEnemyScript != null)
            {
                Debug.Log("AuraDamage Exit");
                chainEnemyScript.RefreshAI();
                chainEnemyScript.target = collision.transform;
            }
        }
    }
}
