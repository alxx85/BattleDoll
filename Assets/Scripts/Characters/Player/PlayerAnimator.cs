using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMover))]
public class PlayerAnimator : MonoBehaviour
{
    private Animator _animator;
    private PlayerMover _playerMover;

    void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
        _playerMover = GetComponent<PlayerMover>();
    }

    private void OnEnable()
    {
        _playerMover.ChangeMoveDirection += OnChangeMoveDirection;
        _playerMover.PlayerAttacks += OnPlayerAttacks;
    }

    private void OnDisable()
    {
        _playerMover.ChangeMoveDirection -= OnChangeMoveDirection;
        _playerMover.PlayerAttacks -= OnPlayerAttacks;
    }

    private void OnPlayerAttacks()
    {
        _animator.SetTrigger("Attack");
    }

    private void OnChangeMoveDirection(float directionX, float directionZ)
    {
        _animator.SetFloat("DirectionX", directionX);
        _animator.SetFloat("DirectionZ", directionZ);
    }

}
