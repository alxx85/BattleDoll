using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationAttack : MonoBehaviour
{
    private PlayerAttack _player;
    private EnemyTargetAttack _enemy;
    private bool IsAquireCharacter = false;

    private void Awake()
    {
        _player = GetComponentInParent<PlayerAttack>();

        if (_player == null)
        {
            _enemy = GetComponentInParent<EnemyTargetAttack>();

            if (_enemy != null)
                IsAquireCharacter = true;
        }
        else
        {
            IsAquireCharacter = true;
        }
    }

    public void OnCrash()
    {
        if (IsAquireCharacter)
        {
            if (_player == null)
                _enemy.AddingCrashEffect();
            else
                _player.AddingCrashEffect();
        }
    }
}
