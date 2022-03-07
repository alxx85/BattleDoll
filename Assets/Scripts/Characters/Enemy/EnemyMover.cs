using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(EnemyTargetAttack))]
public class EnemyMover : MonoBehaviour
{
    [SerializeField] private float _speed;

    private float _currentSpeed;
    private EnemyTargetAttack _enemyAttack;
    private Vector3 _direction;

    private const float Forward = 1f;

    public event UnityAction<float, float> ChangedMoveDirection;

    private void Start()
    {
        _enemyAttack = GetComponent<EnemyTargetAttack>();
        _currentSpeed = _speed;
    }

    private void OnEnable()
    {
        GetComponent<CharacterProperties>().ScaleChanged += OnScaleChanged;
    }

    private void OnDisable()
    {
        GetComponent<CharacterProperties>().ScaleChanged -= OnScaleChanged;
    }

    private void Update()
    {
        if (_enemyAttack.Target != null)
        {
            ChangeEnemyRotation();

            if (Vector3.Distance(transform.position, _enemyAttack.Target.transform.position) > _enemyAttack.AttackDistance)
            {
                if (_enemyAttack.IsAttacking == false)
                {
                    transform.Translate(Vector3.forward * _currentSpeed * Time.deltaTime);
                    
                    if (_direction != transform.forward)
                    {
                        _direction = transform.forward;
                        ChangedMoveDirection?.Invoke(0, Forward);
                    }
                }
            }
        }
        else
        {
            ChangedMoveDirection?.Invoke(0, 0);
        }
    }

    private void ChangeEnemyRotation()
    {
        transform.LookAt(_enemyAttack.Target.transform);
    }

    private void OnScaleChanged(float newScale)
    {
        _currentSpeed = _speed + _speed * newScale;
    }
}
