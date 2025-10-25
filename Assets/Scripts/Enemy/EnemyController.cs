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
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private List<Vector2> pratolPoints;
    [SerializeField] private bool pratolInCircles = true;
    private int _currentPatrolIndex = 0;
    private int _currentPatrolDirection = 1;
    
    private Vector2 _destiny = new Vector2(3,3);

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        MoveTo(GetDestination());
    }

    private Vector2 GetDestination()
    {
        Vector2 currentPatrolPoint = pratolPoints[_currentPatrolIndex];
        if (Vector2.Distance(_rigidbody.position, currentPatrolPoint) <= 0.5f)
        {
            if (pratolInCircles)
            {
                _currentPatrolIndex = _currentPatrolIndex + 1 >= pratolPoints.Count ? 0 : _currentPatrolIndex + 1;
            }
            else
            {
                var nextIndex = _currentPatrolIndex + _currentPatrolDirection;
                if (nextIndex >= pratolPoints.Count || nextIndex < 0)
                {
                    _currentPatrolDirection *= -1;
                } 
                _currentPatrolIndex += _currentPatrolDirection;
            }
            return pratolPoints[_currentPatrolIndex];
        }
        else
        {
            return currentPatrolPoint;
        }
    }

    private void MoveTo(Vector2 destiny)
    {
        var moveVector = (destiny - _rigidbody.position).normalized * moveSpeed;
        _rigidbody.velocity = moveVector;
        
        SetAnimatorVariables(moveVector);
    }

    private void SetAnimatorVariables(Vector2 moveVector)
    {
        animator.SetFloat(Horizontal, moveVector.x);
        animator.SetFloat(Vertical, moveVector.y);
        animator.SetFloat(Speed, moveVector.SqrMagnitude());
    }
    
}