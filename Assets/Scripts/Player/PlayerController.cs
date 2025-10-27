using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private static readonly int Horizontal = Animator.StringToHash("Horizontal");
    private static readonly int Vertical = Animator.StringToHash("Vertical");
    private static readonly int Speed = Animator.StringToHash("Speed");
    private static readonly int LastVertical = Animator.StringToHash("LastVertical");
    private static readonly int LastHorizontal = Animator.StringToHash("LastHorizontal");
    private Rigidbody2D _rigidbody;
    private Vector2 _moveInput;
    
    public Vector2 facingDirection;

    [SerializeField] private Animator animator;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float screenBorder = 2f;

    private Camera _camera; 

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _camera = Camera.main;
        PlayerStats.GameObject = gameObject;
        
        EventSubscriber<GameObject>.Subscribe(GameEvent.OnDied, OnDied);
    }

    private void FixedUpdate()
    {

        // SetMoveVector();
        _rigidbody.velocity = _moveInput * (moveSpeed);
        PreventPlayerGoingOffScreen();
        SetAnimator();
    }

    private void SetAnimator()
    {
        animator.SetFloat(Horizontal, _moveInput.x);
        animator.SetFloat(Vertical, _moveInput.y);
        animator.SetFloat(Speed, _moveInput.SqrMagnitude());
        animator.SetFloat(LastHorizontal,  PlayerStats.FacingDirection.x);
        animator.SetFloat(LastVertical,  PlayerStats.FacingDirection.y);
    }

    private void SetMoveVector()
    {
        // this allows diagonal facing i'd love to see a ways to do this with inputsystem
        _moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        if (_moveInput != Vector2.zero)
        {
            PlayerStats.FacingDirection = _moveInput.normalized;
        }
    }

    private void PreventPlayerGoingOffScreen()
    {
        Vector2 screenPos = _camera.WorldToScreenPoint(transform.position);

        if ((screenPos.x < screenBorder && _moveInput.x < 0) || (screenPos.x > _camera.pixelWidth - screenBorder && _moveInput.x > 0))
        {
            _rigidbody.velocity = new Vector2(0, _moveInput.y);
        }
        
        if ((screenPos.y < screenBorder && _moveInput.y < 0) || (screenPos.y > _camera.pixelHeight - screenBorder && _moveInput.y > 0))
        {
            _rigidbody.velocity = new Vector2(_moveInput.x, 0);
        }
    }


    private void OnMove(InputValue inputValue)
    {
        
        _moveInput = inputValue.Get<Vector2>();
        if (_moveInput != Vector2.zero)
        {
            PlayerStats.FacingDirection = _moveInput.normalized;
        }
        
    }

    private void OnDied(GameObject healthGameObject)
    {
        if (healthGameObject.GetComponent<PlayerController>())
        {   
            Debug.Log("player died");
            Time.timeScale = 0;
        }
    }
}
