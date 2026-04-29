using UnityEngine;

public class EraserDrag : MonoBehaviour
{
    [Header("Камера")]
    public Camera cam;

    [Header("Партиклы")]
    [SerializeField] private ParticleSystem particlesMain;
    [SerializeField] private ParticleSystem particlesSecondary;

    private Vector3 startPosition;
    private bool isDragging;
    private bool isTouchingErasable;

    private void Awake()
    {
        // если не назначили вручную — найдём автоматически (включая неактивные)
        if (particlesMain == null || particlesSecondary == null)
        {
            var systems = GetComponentsInChildren<ParticleSystem>(true);

            if (systems.Length > 0)
                particlesMain = systems[0];

            if (systems.Length > 1)
                particlesSecondary = systems[1];
        }
        Debug.Log(particlesMain);
        Debug.Log(particlesSecondary);
    }

    private void Start()
    {
        startPosition = transform.position;

        if (cam == null)
            cam = Camera.main;

        StopParticles();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isDragging = true;
        }

        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
            StopParticles();
            ReturnToStart();
        }

        if (isDragging)
        {
            MoveToMouse();
        }
        
        if (isDragging)
            StartParticles();
        else
            StopParticles();

      
      
    }

    private void MoveToMouse()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 10f;

        Vector3 worldPos = cam.ScreenToWorldPoint(mousePos);
        transform.position = worldPos;
    }

    private void ReturnToStart()
    {
        transform.position = startPosition;
    }

    private void StartParticles()
    {
        if (particlesMain != null && !particlesMain.isPlaying)
            particlesMain.Play();

        if (particlesSecondary != null && !particlesSecondary.isPlaying)
            particlesSecondary.Play();
    }

    private void StopParticles()
    {
        if (particlesMain != null && particlesMain.isPlaying)
            particlesMain.Stop();

        if (particlesSecondary != null && particlesSecondary.isPlaying)
            particlesSecondary.Stop();
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Erasable"))
        {
            isTouchingErasable = true;

            var obj = other.GetComponent<ErasableObject>();
            if (obj != null)
            {
                obj.ApplyErase();
            }
        }
    }
}