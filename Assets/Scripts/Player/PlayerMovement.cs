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
    public float dashSpeed = 500f; // Placeholder for dash speed, not implemented yet    

    public float rotationAngle = 0f;
    public float dashDuration = 1f; // Placeholder for dash duration, not implemented yet
    public float dashCooldown = 1f; // Placeholder for dash cooldown, not implemented yet

    public float shootCooldown = 0.5f; // Placeholder for shoot cooldown, not implemented yet
    public float shootRange = 30f; // Placeholder for shoot range, not implemented yet

    public float shotDuration = 0.05f;
    public float shotCooldown = 0.5f; // Placeholder for shot cooldown, not implemented yet
    public float reloadTime = 2f;
    public bool isReloading = false; // Placeholder for reloading state, not implemented yet
    public int maxAmmo = 10; // Placeholder for maximum ammo, not implemented yet
    public int currentAmmo = 10; // Placeholder for current ammo, not implemented yet

    public bool ansia = false;
    public bool depressione = false;
    public bool schizofrenia = false;
    public bool morto = false;

    public Rigidbody2D rb;

    private string lastDirection = "none"; // Placeholder for last direction, not implemented yet


    public Transform shootPoint; // Placeholder for shoot point, not implemented yet

    public enum PlayerState
    {
        Idle,
        Moving,
        Shooting,
        Dashing
    }
    public PlayerState playerState = PlayerState.Idle;


    private KeyCode ShootKey = KeyCode.Mouse0;
    private KeyCode DashKey = KeyCode.Space; // Placeholder for dash key, not implemented yet
    public LayerMask enemyLayer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Time.fixedDeltaTime = 0.016f; // ~60 FixedUpdates/sec (anziché 50)
    
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal"); // A e D
        movement.y = Input.GetAxisRaw("Vertical");   // W e S

       
            isMovingHorz = movement.x != 0;
            isMovingVert = movement.y != 0;

        if (isMovingHorz)
        {
            lastDirection = movement.x > 0 ? "right" : "left"; // Update last direction based on horizontal movement
        }
        if (isMovingVert)
        {
            lastDirection = movement.y > 0 ? "up" : "down"; // Update last direction based on vertical movement
        }
        if (isMovingHorz && isMovingVert) {
            if (movement.x>0 && movement.y >0)
            {
                lastDirection = "up-right"; // Update last direction for diagonal movement
            }
            else if (movement.x < 0 && movement.y > 0)
            {
                lastDirection = "up-left"; // Update last direction for diagonal movement
            }
            else if (movement.x < 0 && movement.y < 0)
            {
                lastDirection = "down-left"; // Update last direction for diagonal movement
            }
            else if (movement.x > 0 && movement.y < 0)
            {
                lastDirection = "down-right"; // Update last direction for diagonal movement
            }
        }



        if (isMovingHorz && isMovingVert)
        {
            movement.Normalize(); // Normalize diagonal movement to maintain speed
        }

        if (movement.x != 0 || movement.y != 0 && (playerState != PlayerState.Shooting || playerState != PlayerState.Dashing))
        {
            playerState = PlayerState.Moving;
        }
        else
        {
            playerState = PlayerState.Idle; // Player is idle
        }

        if (Input.GetKeyDown(ShootKey))
        {
            if (playerState == PlayerState.Shooting || playerState == PlayerState.Dashing || isReloading || currentAmmo <= 0)
            {
                return; // Prevent shooting while already shooting or dashing
            }
            Shoot();
            currentAmmo--;
            if (currentAmmo <= 0)
            {
                Invoke("Reload", 0.5f); // Automatically reload if ammo is depleted
            }
            playerState = PlayerState.Shooting; // Set state to Shooting when the player shoots
        }

        if (Input.GetKeyDown(DashKey))
        {
            if (playerState == PlayerState.Dashing || playerState == PlayerState.Shooting || isReloading)
            {
                return; // Prevent dashing while already dashing or shooting
            }
            //Dash();
            playerState = PlayerState.Dashing; // Set state to Dashing when the player dashes
            Invoke("setIdleState", dashDuration); // Reset state to Idle after dash duration

        }
        if (playerState != PlayerState.Dashing )
        {
           
            //plrTransform.position += new Vector3(movement.x, movement.y, 0) * speed * Time.deltaTime;
            RotateToMouse();
        }
        if (playerState == PlayerState.Dashing)
        {
            Dash();
        }



        }

    private void RotateToMouse()
    {
        Vector3 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = mousePosition - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + rotationAngle;
        Quaternion targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }


    private void Shoot()
    {
        RaycastHit2D hit = Physics2D.Raycast(shootPoint.position, shootPoint.up * -1, 30f, enemyLayer);
        ShowLaser(shootPoint.position, shootPoint.position + shootPoint.up * -1 * 30f);
        if (hit.collider != null)
        {
            Debug.Log("Hit: " + hit.collider.name);
            int hitLayer = hit.collider.gameObject.layer;
            string layerName = LayerMask.LayerToName(hitLayer);
            if (layerName == "Enemy")
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
        playerState = PlayerState.Idle; // Reset state to Idle after shooting
    }

    private void Dash()
    {
        print(lastDirection);
        if (lastDirection == "up")
        {
            plrTransform.position += Vector3.up * dashSpeed * Time.deltaTime;
        }
        else if (lastDirection == "down")
        {
            plrTransform.position += Vector3.down * dashSpeed * Time.deltaTime;
        }
        else if (lastDirection == "left")
        {
            plrTransform.position += Vector3.left * dashSpeed * Time.deltaTime;
        }
        else if (lastDirection == "right")
        {
            plrTransform.position += Vector3.right * dashSpeed * Time.deltaTime;
        }
        else if (lastDirection == "up-right")
        {
            plrTransform.position += (Vector3.up + Vector3.right).normalized * dashSpeed * Time.deltaTime;
        }
        else if (lastDirection == "up-left")
        {
            plrTransform.position += (Vector3.up + Vector3.left).normalized * dashSpeed * Time.deltaTime;
        }
        else if (lastDirection == "down-left")
        {
            plrTransform.position += (Vector3.down + Vector3.left).normalized * dashSpeed * Time.deltaTime;
        }
        else if (lastDirection == "down-right")
        {
            plrTransform.position += (Vector3.down + Vector3.right).normalized * dashSpeed * Time.deltaTime;
        }


    }

    private void Reload()
    {
        if (isReloading || currentAmmo >= maxAmmo)
        {
            return; // Prevent reloading if already reloading or ammo is full
        }
        isReloading = true;
        Invoke("FinishReload", reloadTime);
    }

    private void FinishReload()
    {
        currentAmmo = maxAmmo; // Reset current ammo to max ammo
        isReloading = false; // Reset reloading state
    }

    private void setIdleState()
    {
        playerState = PlayerState.Idle; // Set the player state to Idle
    }

    void FixedUpdate()
    {
        movement.x *= speed; // Scale movement by speed and fixed delta time
        movement.y *= speed; // Scale movement by speed and fixed delta time
        // Applica il movimento in FixedUpdate (fisica stabile)
        //rb.MovePosition(plrTransform.position +  new Vector3(movement.x, movement.y, 0) * speed * Time.fixedDeltaTime); // Move the player based on input
        rb.linearVelocity = new Vector2(movement.x * speed, movement.y * speed);
    }
}
