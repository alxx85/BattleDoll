using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMover))]
public class PlayerAttack : Attack
{
    private PlayerMover _mover;

    protected override void OnEnable()
    {
        base.OnEnable();
        _mover = GetComponent<PlayerMover>();
        _mover.PlayerAttacked += AttackEnemys;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        _mover.PlayerAttacked -= AttackEnemys;
    }

    private void Update()
    {
        if (_isAttacking == false && _attack != null)
        {
            StopCoroutine(_attack);
            _attack = null;
        }
    }

    public void AttackEnemys()
    {
        if (_attack == null)
        {
            _attack = StartCoroutine(DealAreaDamage());
            _isAttacking = true;
        }
    }
}
