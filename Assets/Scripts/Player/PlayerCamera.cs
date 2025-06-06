using Unity.VisualScripting;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public Transform playerTransform; // Reference to the playerTransform's transform
    public Transform cameraTransform;
    public float smoothSpeed = 5f;       // Velocità di inseguimento
    public Vector3 offset = new Vector3(0, 0, -5);  // Distanza dalla camera
    public float startDelay = 0.3f;      // Ritardo quando il playerTransform si muove
    public float stopDelay = 0.5f;       // Ritardo quando il playerTransform si ferma

    private Vector3 _velocity;
    private bool _playerIsMoving;
    private float _delayTimer;
    private Vector3 _lastPosition;

    void Start()
    {
        if (playerTransform == null)
            playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        _lastPosition = playerTransform.position;
        transform.position = playerTransform.position + offset;
    }

    void LateUpdate()
    {
        // Controlla se il playerTransform si sta muovendo
        bool isMoving = Vector3.Distance(playerTransform.position, _lastPosition) > 0.01f;
        _lastPosition = playerTransform.position;

        // Gestisci i ritardi
        if (isMoving && !_playerIsMoving)
        {
            _playerIsMoving = true;
            _delayTimer = startDelay;
        }
        else if (!isMoving && _playerIsMoving)
        {
            _playerIsMoving = false;
            _delayTimer = stopDelay;
        }

        // Calcola la posizione target
        Vector3 targetPosition = playerTransform.position + offset;

        // Applica il movimento smooth con ritardo
        float currentSpeed = smoothSpeed;
        if (_delayTimer > 0)
        {
            currentSpeed = smoothSpeed * 0.5f;  // Rallenta durante i ritardi
            _delayTimer -= Time.deltaTime;
        }

        transform.position = Vector3.SmoothDamp(
            transform.position,
            targetPosition,
            ref _velocity,
            1f / currentSpeed
        );

        // Fai guardare la camera al playerTransform
        //transform.LookAt(playerTransform);
    }



}
