using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private static readonly int Horizontal = Animator.StringToHash("Horizontal");
    private static readonly int Vertical = Animator.StringToHash("Vertical");
    private static readonly int Speed = Animator.StringToHash("Speed");
    private Rigidbody2D _rigidbody;
    private Vector2 _moveInput;

    [SerializeField] private Animator animator;
    [SerializeField] private float moveSpeed = 5f; 

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        _rigidbody.velocity = _moveInput * (moveSpeed);
        
        animator.SetFloat(Horizontal, _moveInput.x);
        animator.SetFloat(Vertical, _moveInput.y);
        animator.SetFloat(Speed, _moveInput.SqrMagnitude());
    }

    private void OnMove(InputValue inputValue)
    {
        _moveInput = inputValue.Get<Vector2>();
    }
}
