using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Enemy : MonoBehaviour
{
    public float speed = 1f;
    public float dashSpeed = 5f;
    public float refreshAIcooldown = 0.1f;
    public float followRange = 5f;
    public float dashRange = 0.5f;
    public float dashDuration = 0.3f;

    bool isDashing = false;
    bool avoidingObstacle = false;
    

    public float rotationAngle = 90f;
    public float rotationSpeed = 50f;

    public Transform target;
    Animator anim;
    Rigidbody2D rb;


    Vector2 movement;
    Vector2 avoidanceDirection = Vector2.zero;

    LayerMask obstacleLayer;

    private void Awake()
    {

        //target = GameObject.Find("Player").transform;
        
        rb = GetComponent<Rigidbody2D>();


        anim = this.GetComponentInChildren<Animator>();

        

        obstacleLayer = LayerMask.GetMask("Ground");

    }
    private void RotateToPlr()
    {
        if (target == null) return; // Ensure target is not null to avoid errors
        Vector3 plrPosition = target.position;
        Vector2 direction = plrPosition - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + rotationAngle;
        Quaternion targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    void Start()
    {
        InvokeRepeating(nameof(RefreshAI), 0, refreshAIcooldown);
    }

    void SetTrigger(string s)
    {
        if(anim != null)
        {
            anim.SetTrigger(s);
        }
    }

    void Update()
    {
        RotateToPlr();
        if (!isDashing)
        {
            rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
            SetTrigger("walk");
        }
        else
        {
            SetTrigger("idle");
        }
    }

    public void RefreshAI()
    {

        if (target == null)
        {
            
            movement = Vector2.zero;
            return;
        }

        


        float distance = Vector2.Distance(transform.position, target.position);

        if (distance <= dashRange && !isDashing)
        {
            StartCoroutine(Dash());
            avoidingObstacle = false;
            return;
        }

        if (distance > followRange || isDashing)
        {
            movement = Vector2.zero;
            avoidingObstacle = false;
            return;
        }

        Vector2 toPlayer = ((Vector2)(target.position) - (Vector2)transform.position).normalized;
        LayerMask obstacleLayer = LayerMask.GetMask("Obstacles");
        float rayDistance = 0.8f;


        bool IsBlocked(Vector2 dir)
        {
            return Physics2D.Raycast(transform.position, dir, rayDistance, obstacleLayer);
        }

        if (!avoidingObstacle)
        {

            if (IsBlocked(toPlayer))
            {

                Vector2 rightDir = new Vector2(toPlayer.y, -toPlayer.x);
                Vector2 leftDir = -rightDir;
                Vector2 upDir = new Vector2(-toPlayer.y, toPlayer.x);
                Vector2 downDir = -upDir;

                bool rightBlocked = IsBlocked(rightDir);
                bool leftBlocked = IsBlocked(leftDir);
                bool upBlocked = IsBlocked(upDir);
                bool downBlocked = IsBlocked(downDir);

                if (!rightBlocked)
                {
                    avoidanceDirection = rightDir;
                }
                else if (!leftBlocked)
                {
                    avoidanceDirection = leftDir;
                }

                avoidingObstacle = true;
                movement = avoidanceDirection;
            }
            else
            {

                movement = toPlayer;
            }
        }
        else
        {

            if (!IsBlocked(toPlayer))
            {

                avoidingObstacle = false;
                movement = toPlayer;
            }
            else
            {

                if (IsBlocked(avoidanceDirection))
                {

                    Vector2 rightDir = new Vector2(toPlayer.y, -toPlayer.x);
                    Vector2 leftDir = -rightDir;
                    Vector2 upDir = new Vector2(-toPlayer.y, toPlayer.x);
                    Vector2 downDir = -upDir;

                    if (avoidanceDirection == rightDir && !IsBlocked(leftDir))
                    {
                        avoidanceDirection = leftDir;
                    }
                    else if (avoidanceDirection == leftDir && !IsBlocked(rightDir))
                    {
                        avoidanceDirection = rightDir;
                    }
                    else if (avoidanceDirection == upDir && !IsBlocked(upDir))
                    {
                        avoidanceDirection = downDir;
                    }
                    else if (avoidanceDirection == downDir && !IsBlocked(downDir))
                    {
                        avoidanceDirection = upDir;
                    }
                }

                movement = avoidanceDirection;
            }
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (collision.gameObject.GetComponent<PlayerMovement>().morto) 
            {
                return;
            }
            collision.gameObject.GetComponent<PlayerMovement>().deathAnim();
            
        }
    }

    public void gotPlrTransform()
    {
        RefreshAI();
    }
    IEnumerator Dash()
    {
        if (anim != null)
        {
            SetTrigger("dash");
        }
        isDashing = true;


        Vector2 dashDirection = (target.position - transform.position).normalized;

        float elapsed = 0f;

        while (elapsed < dashDuration)
        {
            rb.MovePosition(rb.position + dashDirection * dashSpeed * Time.fixedDeltaTime);

            elapsed += Time.fixedDeltaTime;


            yield return new WaitForFixedUpdate();
        }


        isDashing = false;
    }


}