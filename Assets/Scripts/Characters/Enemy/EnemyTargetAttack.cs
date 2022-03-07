using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyTargetAttack : Attack
{
    [SerializeField] private float _searchDistance;

    private CharacterProperties _target;
    private float _attackinDelay;
    private Coroutine _waitAttackin;
    private WaitForSeconds _beforeAttackingDelay;

    private const float MaxAttackDelay = 0.5f;
    private const float MinSearchDistance = 1f;

    public CharacterProperties Target => _target;
    public float AttackDistance => _attackPoint.localPosition.z;

    public event UnityAction EnemyAttacked;

    protected override void Start()
    {
        base.Start();
        _attackinDelay = Random.Range(0, MaxAttackDelay);
        _beforeAttackingDelay = new WaitForSeconds(_attackinDelay);
    }

    private void Update()
    {
        if (_target == null)
            _target = AcquireTarget();
        else
        {
            if (Vector3.Distance(transform.position, _target.transform.position) <= _attackPoint.localPosition.z + _scale)
            {
                if (_waitAttackin == null && _attack == null)
                {
                    _isAttacking = true;
                    _waitAttackin = StartCoroutine(BeforeAttackWait());
                }
                else if (_isAttacking == false)
                {
                    StopCoroutine(_attack);
                    _waitAttackin = null;
                    _attack = null;
                }
            }
        }
    }

    private IEnumerator BeforeAttackWait()
    {
        yield return _beforeAttackingDelay;

        if (_attack == null)
        {
            _attack = StartCoroutine(DealAreaDamage());
            EnemyAttacked?.Invoke();
        }
        StopCoroutine(_waitAttackin);
    }

    private CharacterProperties AcquireTarget()
    {
        CharacterProperties selfTarget = transform.GetComponent<CharacterProperties>();
        float currentSearchDistance = 1f;

        do
        {
            Collider[] targets = Physics.OverlapSphere(transform.position, currentSearchDistance + _scale);

            for (int i = 0; i < targets.Length; i++)
            {
                if (targets[i].TryGetComponent(out CharacterProperties enemy))
                    if (enemy != selfTarget)
                        return enemy;
            }
            currentSearchDistance++;
            Mathf.Clamp(currentSearchDistance, MinSearchDistance, _searchDistance);

        } while (currentSearchDistance < _searchDistance);
        return null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(_attackPoint.position, _attackRange);
    }
}
