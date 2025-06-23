using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Candle : MonoBehaviour
{
    [Header("Parameters")]
    PlayerMovement playerMovement = null;
    public float removeDebuffTimer = 2f;
    public float speed = 0.3f;

    [Header("Animations")]
    public Animator chargeAnimation;

    bool charging;
    Vector2 movementVector = Vector2.zero;
    Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        float randX = Random.Range(-1.0f, 1.0f);
        float randY = Random.Range(-1.0f, 1.0f);
        movementVector = new Vector2(randX, randY);
    }

    private void Update()
    {
        MoveSlowly();
        if (charging)
        {
            //chargeAnimation.SetTrigger("charge");
        }
    }

    
    void MoveSlowly()
    {
        rb.linearVelocity = movementVector * speed;
    }

    void RemoveDebuff()
    {
        if (charging)
        {
            playerMovement.ansia = false;
            playerMovement.depressione = false;
            charging = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (collision.GetComponent<PlayerMovement>() != null)
            { 
                playerMovement = collision.GetComponent<PlayerMovement>();
                if(playerMovement.ansia || playerMovement.depressione)
                {
                    charging = true;
                    chargeAnimation.SetTrigger("charge");
                    Invoke("RemoveDebuff", removeDebuffTimer);
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            charging = false;
        }
    }
}