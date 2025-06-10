using UnityEngine;

public class auraEnemy : MonoBehaviour
{
    public float refreshAIcooldown = 0.1f;
    public float followRange = 5f;
    public float speed = 1f;
    public float stopDistance = 0.4f;

    bool avoidingObstacle = false;
    bool playerAlive = true;

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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InvokeRepeating(nameof(RefreshAI), 0, refreshAIcooldown);
    }

    // Update is called once per frame
    void Update()
    {
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
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
}
