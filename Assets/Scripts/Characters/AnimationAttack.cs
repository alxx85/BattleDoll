using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationAttack : MonoBehaviour
{
    private PlayerAttack _player;
    private EnemyTargetAttack _enemy;
    private bool _isAquireCharacter = false;

    private void Awake()
    {
        _player = GetComponentInParent<PlayerAttack>();

        if (_player == null)
        {
            _enemy = GetComponentInParent<EnemyTargetAttack>();

            if (_enemy != null)
                _isAquireCharacter = true;
        }
        else
        {
            _isAquireCharacter = true;
        }
    }

    public void OnCrash()
    {
        if (_isAquireCharacter)
        {
            if (_player == null)
                _enemy.AddCrashEffect();
            else
                _player.AddCrashEffect();
        }
    }
}
