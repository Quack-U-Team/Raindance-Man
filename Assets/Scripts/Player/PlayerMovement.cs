using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Transform plrTransform;
    public float speed = 5f;
    private Vector2 movement;
    public Camera mainCamera;
    public float rotationSpeed = 90f;
    public LineRenderer lineRenderer;
    public bool isMovingVert = false;
    public bool isMovingHorz = false;

    private KeyCode ShootKey = KeyCode.Mouse0;
    public LayerMask enemyLayer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal"); // A e D
        movement.y = Input.GetAxisRaw("Vertical");   // W e S

        isMovingHorz = movement.x != 0;
        isMovingVert = movement.y != 0;



        plrTransform.position += new Vector3(movement.x, movement.y, 0) * speed * Time.deltaTime;
        RotateToMouse();

        if (Input.GetKeyDown(ShootKey))
        {
            Shoot();
        }
    }

    private void RotateToMouse()
    {
        Vector3 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = mousePosition - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        Quaternion targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }


    private void Shoot()
    {
        RaycastHit2D hit = Physics2D.Raycast(plrTransform.position, plrTransform.up, 30f, enemyLayer);
        ShowLaser(plrTransform.position, plrTransform.position + plrTransform.up * 30f);
        if (hit.collider != null)
        {
            Debug.Log("Hit: " + hit.collider.name);
            int hitLayer = hit.collider.gameObject.layer;
            string layerName = LayerMask.LayerToName(hitLayer);
            if ( layerName == "Enemy")
            {
                
                Destroy(hit.collider.gameObject); // Assuming the enemy has a script that handles its destruction
            }
            
            
        }
        else
        {
            Debug.Log("Missed!");
        }

    }

    private void ShowLaser(Vector3 start, Vector3 end)
    {
        lineRenderer.SetPosition(0, start);
        lineRenderer.SetPosition(1, end);
        lineRenderer.enabled = true;
        Invoke("HideLaser", 0.05f);
    }

    private void HideLaser()
    {
        lineRenderer.enabled = false;
    }
}
