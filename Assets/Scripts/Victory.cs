using UnityEngine;
using UnityEngine.SceneManagement;

public class Victory : MonoBehaviour
{

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(UnityEngine.Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {

            PlayerMovement plrMovement = collision.gameObject.GetComponent<PlayerMovement>();
            if ((plrMovement != null) && (plrMovement.collectiblesFound == 3))
            {

            }

        }

    }
}
