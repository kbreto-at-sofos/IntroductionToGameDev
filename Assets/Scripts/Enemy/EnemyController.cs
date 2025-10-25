using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private static readonly int Horizontal = Animator.StringToHash("Horizontal");
    private static readonly int Vertical = Animator.StringToHash("Vertical");
    private static readonly int Speed = Animator.StringToHash("Speed");
    private Rigidbody2D _rigidbody;
    [SerializeField] private Animator animator;
    [SerializeField] private float moveSpeed = 5f;
    private Vector2 _destiny = new Vector2(3f, 3f);

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (Vector2.Distance(_rigidbody.position, _destiny) > 0.5f)
        {
            var moveVector = (_destiny - _rigidbody.position).normalized * moveSpeed;
            _rigidbody.velocity = moveVector;
            
            animator.SetFloat(Horizontal, moveVector.x);
            animator.SetFloat(Vertical, moveVector.y);
            animator.SetFloat(Speed, moveVector.SqrMagnitude());
        }
        else
        {
            _destiny = -_destiny;
        }
    }
}