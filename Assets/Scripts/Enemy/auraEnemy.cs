using UnityEngine;

public class auraEnemy : MonoBehaviour
{
    public float refreshAIcooldown = 0.1f;
    public float followRange = 3f;
    public float speed = 8f;
    public float stopDistance = 0.4f;
    public float coolDownAnsia = 5;

    bool avoidingObstacle = false;
    bool playerAlive = true;

    Rigidbody2D rb;

    public PlayerMovement playerScript;

    public Transform target;
    public Vector3 targetPosition;

    public float rotationAngle = 90f;
    public float rotationSpeed = 50f;

    Vector2 movement;
    Vector2 avoidanceDirection = Vector2.zero;

    public float dannoMentale = 1.0f;

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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InvokeRepeating(nameof(RefreshAI), 0, refreshAIcooldown);
    }

    // Update is called once per frame
    void Update()
    {
        RotateToPlr();
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

            playerScript.ansia = true;
            playerScript.mentalPointsRemove(10);
            playerScript.deathAnim();

        }
    }

}
