using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMover))]
public class PlayerAnimator : MonoBehaviour
{
    private Animator _animator;
    private PlayerMover _playerMover;

    private const string Attack = "Attack";
    private const string DirectionX = "DirectionX";
    private const string DirectionZ = "DirectionZ";

    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
        _playerMover = GetComponent<PlayerMover>();
    }

    private void OnEnable()
    {
        _playerMover.ChangedMoveDirection += OnChangedMoveDirection;
        _playerMover.MakeDamage += OnPlayerAttacked;
    }

    private void OnDisable()
    {
        _playerMover.ChangedMoveDirection -= OnChangedMoveDirection;
        _playerMover.MakeDamage -= OnPlayerAttacked;
    }

    private void OnPlayerAttacked()
    {
        _animator.SetTrigger(Attack);
    }

    private void OnChangedMoveDirection(float directionX, float directionZ)
    {
        _animator.SetFloat(DirectionX, directionX);
        _animator.SetFloat(DirectionZ, directionZ);
    }
}
