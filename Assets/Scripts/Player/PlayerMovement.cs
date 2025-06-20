using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    [Header("Audio")]
    public AudioSource shootSound;
    public AudioSource reloadSound;
    public AudioSource deathSound;

    [Header("Animation")]
    private Animator anim;

    [Header("Physics and balance")]
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

    [Header("Status effects")]
    public bool ansia = false;
    public bool depressione = false;
    public bool schizofrenia = false;
    public bool morto = false;

    public float ansiaDuration = 20f; // Placeholder for anxiety duration, not implemented yet
    public float depressioneDuration = 20f; // Placeholder for depression duration, not implemented yet

    public int ansiaMisfireChance = 50; // Placeholder for anxiety misfire chance

    public Rigidbody2D rb;

    public bool canDash = true;
    public int collectiblesFound = 0;
    public float maxSanitaMentale = 100;
    public float sanitaMentale = 100; // Placeholder for sanity, not implemented yet

    public Transform shootPoint; // Placeholder for shoot point, not implemented yet
    private float depresSpeed = 1f; // Placeholder for depression speed, not implemented yet
    public float depresSpeedBase = 0.5f; // Placeholder for base depression speed, not implemented yet
    public enum PlayerState
    {
        Idle,
        Moving,
        Shooting,
        Dashing
    }
    public PlayerState playerState = PlayerState.Idle;


    private KeyCode ShootKey = KeyCode.Mouse0;
    private KeyCode DashKey = KeyCode.Space; 
    public LayerMask enemyLayer;

    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Time.fixedDeltaTime = 0.016f; // ~60 FixedUpdates/sec (anzich� 50)
        anim = this.GetComponentInChildren<Animator>();
        anim.SetTrigger("spawn");
    }

    void BaseAnimations()
    {
        if (!morto)
        { 
            if (playerState == PlayerState.Idle)
            {
                anim.SetTrigger("idle");
            }

            if (playerState == PlayerState.Dashing)
            {
                anim.SetTrigger("roll");
            }

            if (playerState == PlayerState.Shooting)
            {
                anim.SetTrigger("shoot");
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        BaseAnimations();

        if (ansia)
        {
            Invoke("removeAnsia", ansiaDuration); // Remove anxiety after 5 seconds
        }
        if (depressione)
        {
            Invoke("removeDepressione", depressioneDuration); // Remove depression after 5 seconds
        }
       
        if (playerState != PlayerState.Dashing )
        {
            movement.x = Input.GetAxisRaw("Horizontal"); // A e D
            movement.y = Input.GetAxisRaw("Vertical");   // W e S
            
        }
        movement.Normalize(); // Normalize diagonal movement to maintain speed


        isMovingHorz = movement.x != 0;
        isMovingVert = movement.y != 0;

        if (depressione)
        {
            depresSpeed = depresSpeedBase; // Set depression speed to base value
        }
        else
        {
            depresSpeed = 1f; // Reset depression speed if not depressed
        }

        if ((movement.x == 0 && movement.y == 0) && playerState != PlayerState.Dashing && playerState != PlayerState.Shooting)
        {
            playerState = PlayerState.Idle; // Player is idle
        }
        else if ((movement.x != 0 || movement.y != 0) && (playerState != PlayerState.Dashing && playerState != PlayerState.Moving))
        {
            playerState = PlayerState.Moving;
        }
        

        if (Input.GetKeyDown(ShootKey))
        {
            if (playerState == PlayerState.Shooting || playerState == PlayerState.Dashing || isReloading || currentAmmo <= 0)
            {
                return; // Prevent shooting while already shooting or dashing
            }

            if (ansia == false)
            {
                Shoot();
                currentAmmo--;
                if (currentAmmo <= 0)
                {
                    Invoke("Reload", 0.5f); // Automatically reload if ammo is depleted
                }
                playerState = PlayerState.Shooting; // Set state to Shooting when the player shoots
            }
            else
            {
                int randomInt = Random.Range(0, 100);
                if (randomInt < ansiaMisfireChance) // 50% chance to shoot
                {
                    Shoot();
                    currentAmmo--;
                    if (currentAmmo <= 0)
                    {
                        Invoke("Reload", 0.5f); // Automatically reload if ammo is depleted
                    }
                    playerState = PlayerState.Shooting; // Set state to Shooting when the player shoots
                }
                else
                {
                    Debug.Log("Anxiety prevented shooting this time.");
                }
            }

        }

        if (Input.GetKeyDown(DashKey))
        {
            print("DASH PRESSED");
            if (playerState == PlayerState.Dashing || playerState == PlayerState.Shooting || isReloading)
            {
                return; // Prevent dashing while already dashing or shooting
            }
            if (!canDash)
            {
                return; // Prevent dashing if dash is on cooldown
            }
            playerState = PlayerState.Dashing; // Set state to Dashing when the player dashes
            canDash = false; // Set dash cooldown
            Invoke("setIdleState", dashDuration); // Reset state to Idle after dash duration
            Invoke("DashCooldownReset", dashDuration+dashCooldown); // Reset dash cooldown

        }
        if (playerState != PlayerState.Dashing )
        {
            RotateToMouse();
        }



        movement.x *= speed; // Scale movement by speed and fixed delta time
        movement.y *= speed; // Scale movement by speed and fixed delta time
        if (playerState == PlayerState.Dashing)
        {
            movement.x *= dashSpeed;
            movement.y *= dashSpeed; // Apply dash speed to movement
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

    private void DashCooldownReset()
    {
        canDash = true; // Reset dash cooldown
    }

    GameObject hitEnemy;

    void DelayedEnemyDeath()
    {
        Destroy(hitEnemy); // Assuming the enemy has a script that handles its destruction
    }

    private void Shoot()
    {
        shootSound.Play();
        RaycastHit2D hit = Physics2D.Raycast(shootPoint.position, shootPoint.up * -1, shootRange, enemyLayer);

        if (hit.collider != null)
        {
            Vector2 hitPosition = hit.point;
            ShowLaser(shootPoint.position, hitPosition);
            Debug.Log("Hit: " + hit.collider.name);
            int hitLayer = hit.collider.gameObject.layer;
            string layerName = LayerMask.LayerToName(hitLayer);
            if (layerName == "Enemy")
            {
                hitEnemy = hit.collider.gameObject;
                if (hitEnemy.GetComponentInChildren<Animator>() != null)
                {
                    Debug.LogWarning("setTrigger hurt");
                    Animator hitEnemyAnim = hitEnemy.GetComponentInChildren<Animator>();
                    hitEnemyAnim.SetTrigger("hurt");
                }
                Invoke("DelayedEnemyDeath", 0.15f);

                if (!ansia && !depressione)
                {
                    mentalPointsRemove(-5); // Reduce mental points by 10 if not affected by anxiety, depression, or schizophrenia
                }
               
            }
            

        }
        else
        {
            Vector2 endPosition = shootPoint.position + (-shootPoint.up * shootRange);
            ShowLaser(shootPoint.position, endPosition);
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
        reloadSound.Play();
        currentAmmo = maxAmmo; // Reset current ammo to max ammo
        isReloading = false; // Reset reloading state
    }

    private void setIdleState()
    {
        playerState = PlayerState.Idle; // Set the player state to Idle
    }

    void FixedUpdate()
    {
        
        


        rb.linearVelocity = new Vector2(movement.x * speed *depresSpeed, movement.y * speed * depresSpeed);
       
     
    }

    private void removeAnsia()
    {
        ansia = false; // Reset anxiety state
        //Debug.Log("Anxiety removed.");
    }
    private void removeDepressione()
    {
        depressione = false; // Reset anxiety state
        //Debug.Log("Depression removed.");
    }

    public void death()
    {
       Debug.Log("Player is dead.");
       InGameUI.GameOver(); // Call GameOver method from InGameUI
    }

    public void deathAnim()
    {
        morto = true; // Set dead state
        deathSound.Play();
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        anim.SetTrigger("death");
        Invoke("death", 2f); // Call death after 2 seconds
    }

    public void mentalPointsRemove(float dannoMentale)
    {
        sanitaMentale -= dannoMentale; // Reduce mental points by damage
        sanitaMentale = Mathf.Clamp(sanitaMentale, 0, maxSanitaMentale);
        if (sanitaMentale <= 0)
        {
            deathAnim(); // Call death animation if mental points reach zero
        }
    }


}
