using UnityEngine;

public class PlayerVisual : MonoBehaviour
{
    private Animator _animator;

    private static readonly int MoveX = Animator.StringToHash("MoveX");
    private static readonly int MoveY = Animator.StringToHash("MoveY");
    private static readonly int IsMoving = Animator.StringToHash("IsMoving");
    private static readonly int IsRunning = Animator.StringToHash("IsRunning");
   
    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        Vector2 input = PlayerController.Instance.GetInputVector();

        if (input.magnitude > 1f)
            input = input.normalized;
        _animator.SetFloat(MoveX, input.x);
        _animator.SetFloat(MoveY, input.y);
       
        bool isMoving = input.magnitude > 0.1f;
       
        _animator.SetBool(IsMoving, isMoving);
        _animator.SetBool(IsRunning, PlayerController.Instance.IsActuallyRunning());
    }
}