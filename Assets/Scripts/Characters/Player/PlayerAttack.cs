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
        _mover.MakeDamage += AttackEnemys;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        _mover.MakeDamage -= AttackEnemys;
    }

    private void Update()
    {
        if (IsAttacking == false && AttackCoroutine != null)
        {
            StopCoroutine(AttackCoroutine);
            AttackCoroutine = null;
        }
    }

    public void AttackEnemys()
    {
        if (AttackCoroutine == null)
        {
            AttackCoroutine = StartCoroutine(DealAreaDamage());
            IsAttacking = true;
        }
    }
}
