using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationAttack : MonoBehaviour
{
    private PlayerAttack _player;
    private EnemyTargetAttack _enemy;

    private void Awake()
    {
        _player = GetComponentInParent<PlayerAttack>();

        if (_player == null)
            _enemy = GetComponentInParent<EnemyTargetAttack>();
    }

    public void Crash()
    {
        if (_player == null)
            _enemy.CrashEffect();
        else
            _player.CrashEffect();
    }
}
