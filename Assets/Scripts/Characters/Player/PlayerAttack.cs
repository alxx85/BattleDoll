using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterProperties))]
public class PlayerAttack : Attack
{
    protected override void OnEnable()
    {
        base.OnEnable();
        GetComponent<PlayerMover>().PlayerAttacks += AttackEnemys;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        GetComponent<PlayerMover>().PlayerAttacks -= AttackEnemys;
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
            _attack = StartCoroutine(AttackArea());
            _isAttacking = true;
        }
    }
}
