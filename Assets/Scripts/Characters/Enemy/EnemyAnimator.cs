using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyMover), typeof(EnemyTargetAttack))]
public class EnemyAnimator : MonoBehaviour
{
    private Animator _animator;
    private EnemyMover _enemyMover;
    private EnemyTargetAttack _enemyAttack;

    private const string Attack = "Attack";
    private const string DirectionX = "DirectionX";
    private const string DirectionZ = "DirectionZ";

    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
        _enemyMover = GetComponent<EnemyMover>();
        _enemyAttack = GetComponent<EnemyTargetAttack>();
    }

    private void OnEnable()
    {
        _enemyMover.ChangedMoveDirection += OnChangedMoveDirection;
        _enemyAttack.MakeDamage += OnEnemyAttacked;
    }

    private void OnDisable()
    {
        _enemyMover.ChangedMoveDirection -= OnChangedMoveDirection;
        _enemyAttack.MakeDamage -= OnEnemyAttacked;
    }

    private void OnEnemyAttacked()
    {
        _animator.SetTrigger(Attack);
    }

    private void OnChangedMoveDirection(float directionX, float directionZ)
    {
        _animator.SetFloat(DirectionX, directionX);
        _animator.SetFloat(DirectionZ, directionZ);
    }
}
