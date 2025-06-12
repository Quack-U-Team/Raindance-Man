using UnityEngine;

public class PlayerMovementTest : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rb;
    private float inputX, inputY;

    private void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        inputX = Input.GetAxisRaw("Horizontal");
        inputY = Input.GetAxisRaw("Vertical");
    }
    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(inputX * speed, inputY * speed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        print("ora spiegami com'è possibile che detecti la collisione ma non fermi il personaggio");
    }
}
