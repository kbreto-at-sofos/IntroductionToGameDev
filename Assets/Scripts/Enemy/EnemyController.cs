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
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private List<Vector2> pratolPoints;
    [SerializeField] private bool pratolInCircles = true;
    private int _currentPatrolIndex = 0;
    private int _currentPatrolDirection = 1;
    
    [Header("Bullet Settings")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float bulletSpeed = 20f;
    [SerializeField] private float bulletDelay = 0.5f;
    [SerializeField] private float playerShootDistance = 5f;
    
    private float _lastFireTime;
    
    

    

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (CanShootPlayer())
        {
            MoveTo(gameObject.transform.position);
            float timeSinceLastFire = Time.time - _lastFireTime;

            if (timeSinceLastFire >= bulletDelay)
            {
                FireBullet();
                _lastFireTime = Time.time;
            }
        }
        else
        {
            MoveTo(GetDestination());
        }
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

    private bool CanShootPlayer()
    {
        return Vector3.Distance(PlayerStats.GameObject.transform.position, gameObject.transform.position) <= playerShootDistance;
    }

    private void FireBullet()
    {
        Vector3 playerPosition = PlayerStats.GameObject.transform.position;
        Vector3 playerDirection = (playerPosition - gameObject.transform.position).normalized;
        
        BulletController.ShootBullet(bulletPrefab, transform.position, playerDirection, bulletSpeed);
    }
    
}