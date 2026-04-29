using UnityEngine;

public class CircleInput : MonoBehaviour
{
    [SerializeField] private Transform center;
    [SerializeField] private float requiredAngle = 300f;

    private bool isHolding;
    private float totalAngle;
    private Vector2 lastDirection;

    private bool isActive = false;

    public System.Action OnCircleComplete;

    public void SetActive(bool state)
    {
        isActive = state;
    }

    void Update()
    {
        if (!isActive) return;

        if (Input.GetMouseButtonDown(0))
        {
            StartCircle();
        }

        if (Input.GetMouseButtonUp(0))
        {
            ResetCircle();
        }

        if (isHolding)
        {
            TrackMovement();
        }
    }

    private void StartCircle()
    {
        isHolding = true;
        totalAngle = 0f;
        lastDirection = GetDirection();
    }

    private void ResetCircle()
    {
        isHolding = false;
        totalAngle = 0f;
    }

    private void TrackMovement()
    {
        Vector2 currentDir = GetDirection();

        float angle = Vector2.SignedAngle(lastDirection, currentDir);

        totalAngle += Mathf.Abs(angle);
        lastDirection = currentDir;

        if (totalAngle >= requiredAngle)
        {
            CompleteCircle();
        }
    }

    private Vector2 GetDirection()
    {
        Vector2 mouseWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return (mouseWorld - (Vector2)center.position).normalized;
    }

    private void CompleteCircle()
    {
        if (!isActive) return;

        isHolding = false;
        totalAngle = 0f;

        Debug.Log("КРУГ СДЕЛАН");

        OnCircleComplete?.Invoke();
    }
}