using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyMover))]
public class EnemyAnimator : MonoBehaviour
{
    private Animator _animator;
    private EnemyMover _enemyMover;
    private EnemyTargetAttack _enemyAttack;

    void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
        _enemyMover = GetComponent<EnemyMover>();
        _enemyAttack = GetComponent<EnemyTargetAttack>();
    }

    private void OnEnable()
    {
        _enemyMover.ChangeMoveDirection += OnChangeMoveDirection;
        _enemyAttack.EnemyAttacks += OnEnemyAttacks;
    }

    private void OnDisable()
    {
        _enemyMover.ChangeMoveDirection -= OnChangeMoveDirection;
        _enemyAttack.EnemyAttacks -= OnEnemyAttacks;
    }

    private void OnEnemyAttacks()
    {
        _animator.SetTrigger("Attack");
    }

    private void OnChangeMoveDirection(float directionX, float directionZ)
    {
        _animator.SetFloat("DirectionX", directionX);
        _animator.SetFloat("DirectionZ", directionZ);
    }
}
