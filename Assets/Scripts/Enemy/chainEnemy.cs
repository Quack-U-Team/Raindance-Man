using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class chainEnemy : MonoBehaviour
{
    public GameObject chainTemplate;


    public float shootCoolDown = 4f;
    public float refreshAIcooldown = 0.1f;
    public float followRange = 5f;
    public float speed = 1f;
    public float stopDistance = 0.4f;

    bool isInCoolDown = false;
    bool avoidingObstacle = false;
    bool playerAlive = true;

    public float rotationAngle = 90f;
    public float rotationSpeed = 50f;

    Rigidbody2D rb;

    public Transform target;
    public Vector3 targetPosition;

    Vector2 movement;
    Vector2 avoidanceDirection = Vector2.zero;

    LayerMask obstacleLayer;
    private void Awake()
    {

        target = GameObject.Find("Player").transform;


        rb = GetComponent<Rigidbody2D>();

        obstacleLayer = LayerMask.GetMask("Obstacles");
    }

    private void RotateToPlr()
    {
        Vector3 plrPosition = target.position;
        Vector2 direction = plrPosition - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + rotationAngle;
        Quaternion targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    void Start()
    {
        StartCoroutine(BrainCoroutine());
        InvokeRepeating(nameof(RefreshAI), 0, refreshAIcooldown);
    }


    void Update()
    {
        RotateToPlr();

        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);

        if (Mathf.Abs(targetPosition.x - transform.position.x) < 0.5f)
        {
            Shoot();
        }

    }

    void RefreshAI()
    {

        if (target == null)
        {
            playerAlive = false;
            movement = Vector2.zero;
            return;
        }

        if (!playerAlive)
        {
            movement = Vector2.zero;
            return;
        }

        float distance = Vector2.Distance(transform.position, target.position);
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

                if (distance < stopDistance)
                {
                    movement = Vector2.zero;
                    return;
                }
            }
        }
        else
        {

            if (!IsBlocked(toPlayer))
            {

                avoidingObstacle = false;
                movement = toPlayer;

                if (distance < stopDistance)
                {
                    movement = Vector2.zero;
                    return;
                }
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

    IEnumerator BrainCoroutine()
    {
        while (true)
        {
            targetPosition = target.position;
            yield return new WaitForSeconds(Random.Range(0.1f, 0.5f));

        }
    }
   
    void Shoot()
    {
        if (isInCoolDown)
            return;

        GameObject newBullet = Instantiate(chainTemplate);
        newBullet.transform.position = transform.position;
        newBullet.SetActive(true);

        Chain chainScript = newBullet.GetComponent<Chain>();
        chainScript.direction = (targetPosition - transform.position).normalized;


        isInCoolDown = true;
        Invoke("ResetCoolDown", shootCoolDown);

    }

    void ResetCoolDown()
    {
        isInCoolDown = false;
    }

}