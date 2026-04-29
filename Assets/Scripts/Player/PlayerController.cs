using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;



[SelectionBase]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private AudioSource footstepAudio;
    [SerializeField] private float stepInterval = 0.5f; // интервал между шагами
    private float stepTimer = 0f;
    public static PlayerController Instance { get; private set; }
    public Vector2 GetInputVector() => _inputVector;
    [SerializeField] public float movingSpeed = 10f;
    [Space(20)]
    public Transform EndSpawnPoint;
    public Transform StartSpawnPoint;
    private Vector2 _inputVector;
    private bool availableRun = false;
    private Rigidbody2D _rb;
    //private KnockBack _knockBack;
    private readonly float _minMovingSpeed = 0.1f;
    private bool _isMoving = false;
    private float _initialMovingSpeed;
    private bool _isBusy = false;
    public bool _isAlive = true;
    private bool _canMove = true;
    private Camera _mainCamera;
    [SerializeField] private float runMultiplier = 2f;
    [SerializeField] private SpriteRenderer playerSprite;
    [SerializeField] private UnityEngine.Rendering.Universal.Light2D flashlight;

    private bool _isRunningInput = false;


    private void Awake()
    {
        Instance = this;
        _rb = GetComponent<Rigidbody2D>();

        _mainCamera = Camera.main;

        _initialMovingSpeed = movingSpeed;
        //TeleportToStartSpawnPoint();
    }

    private void Start()
    {
        //GetComponent<SpriteRenderer>().spriteSortPoint = SpriteSortPoint.Pivot; //йа в отчаянии
        GameInput.Instance.EnableMovement();
        //GameInput.Instance.OnFlashlightToggle += GameInput_OnFlashlightToggle;
        GameInput.Instance.OnRunStarted += GameInput_OnRunStarted;
        GameInput.Instance.OnRunCanceled += GameInput_OnRunCanceled;
 
    }

    private void Update()
    {
        //GetComponent<SpriteRenderer>().spriteSortPoint = SpriteSortPoint.Pivot; //йа в отчаянии
        if (!_canMove)
        {
            _inputVector = Vector2.zero;
            return;
        }

        _inputVector = GameInput.Instance.GetMovementVector();
        availableRun = LevelManager.Instance.CanRun();

        if (!_isAlive || _isBusy) return;
    }

    private void OnDestroy()
    {
        
        if (GameInput.Instance != null)
        {
            GameInput.Instance.OnRunStarted -= GameInput_OnRunStarted;
            GameInput.Instance.OnRunCanceled -= GameInput_OnRunCanceled;
        }
    }

    private void FixedUpdate()
    {
        if (!_isAlive || _isBusy || !_canMove) return;

        HandleMovement();
    }
    public void SetMovementEnabled(bool enabled)
    {
        _canMove = enabled;

        if (!enabled)
        {
            _inputVector = Vector2.zero;
            _rb.velocity = Vector2.zero; // чтобы резко остановить
            _isMoving = false;
        }
    }
    public bool IsMoving()
    {
        return _isMoving;
    }
    public bool IsActuallyRunning()
    {
        return _isRunningInput && LevelManager.Instance.CanRun();
    }
    private void HandleMovement()
    {
        float speed = movingSpeed;

        if (_isRunningInput && LevelManager.Instance.CanRun())
        {
            speed *= runMultiplier;
        }

        _rb.MovePosition(_rb.position + _inputVector * (speed * Time.fixedDeltaTime));
        
        if (Mathf.Abs(_inputVector.x) > _minMovingSpeed || Mathf.Abs(_inputVector.y) > _minMovingSpeed)
        {
            _isMoving = true;
        }
        else
        {
            _isMoving = false;
        }
        //ЗВУЧОК шагов
        /*if (_inputVector.magnitude > 0.1f)
        {
            if (IsActuallyRunning())
                AudioManager.Instance.PlayRun();
            else
                AudioManager.Instance.PlayWalk();
        }
        else
        {
            //AudioManager.Instance.StopSteps();
        }*/
    }
    private void GameInput_OnRunStarted(object sender, EventArgs e)
    {
        _isRunningInput = true;
    }

    private void GameInput_OnRunCanceled(object sender, EventArgs e)
    {
        _isRunningInput = false;
    }
    public Vector3 GetPlayerScreenPosition()
    {
        Vector3 playerScreenPosition = _mainCamera.WorldToScreenPoint(transform.position);
        return playerScreenPosition;
    }
  

    public void TeleportToEndSpawnPoint()
    {
        if (EndSpawnPoint != null)
        {
            transform.position = EndSpawnPoint.position;
            
        }
        else
        {
            Debug.LogWarning("LoseSpawnPoint не назначен в инспекторе!");
        }
    }

    public void TeleportToStartSpawnPoint()
    {
        if (StartSpawnPoint != null)
        {
            transform.position = StartSpawnPoint.position;
            
        }
        else
        {
            Debug.LogWarning("LoseSpawnPoint не назначен в инспекторе!");
        }
    }
}