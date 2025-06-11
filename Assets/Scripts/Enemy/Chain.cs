using Unity.VisualScripting;
using UnityEngine;

public class Chain : MonoBehaviour
{
    public float speed = 10;
    public Vector3 direction;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;

        Vector3 bulletViewportPosition = Camera.main.WorldToViewportPoint(transform.position);

        if ((bulletViewportPosition.x < 0) || (bulletViewportPosition.x > 1) || (bulletViewportPosition.y < 0) || (bulletViewportPosition.y > 1))
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            IHittable hittable = collision.gameObject.GetComponent<IHittable>();
            if (hittable != null)
            {
                hittable.OnHitSuffered();
            }

            Destroy(gameObject);
        }
    }
}
