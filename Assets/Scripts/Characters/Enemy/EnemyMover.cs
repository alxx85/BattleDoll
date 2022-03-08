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
    private EnemyTargetAttack _attack;
    private CharacterProperties _properties;
    private Vector3 _direction;

    private const float Forward = 1f;

    public event UnityAction<float, float> ChangedMoveDirection;

    private void Awake()
    {
        _attack = GetComponent<EnemyTargetAttack>();
        _properties = GetComponent<CharacterProperties>();
        _currentSpeed = _speed;
    }

    private void OnEnable()
    {
        _properties.ScaleChanged += OnScaleChanged;
    }

    private void OnDisable()
    {
        _properties.ScaleChanged -= OnScaleChanged;
    }

    private void Update()
    {
        if (_attack.Target != null)
        {
            ChangeRotation();

            if (Vector3.Distance(transform.position, _attack.Target.transform.position) > _attack.AttackDistance)
            {
                if (_attack.IsAttacking == false)
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

    private void ChangeRotation()
    {
        transform.LookAt(_attack.Target.transform);
    }

    private void OnScaleChanged(float newScale)
    {
        _currentSpeed = _speed + _speed * newScale;
    }
}
