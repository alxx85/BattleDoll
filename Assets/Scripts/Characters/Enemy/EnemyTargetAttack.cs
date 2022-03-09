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
    public float AttackDistance => AttackPoint.localPosition.z;

    public event UnityAction MakeDamage;

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
            if (Vector3.Distance(transform.position, _target.transform.position) <= AttackPoint.localPosition.z + Scale)
            {
                if (_waitAttackin == null && AttackCoroutine == null)
                {
                    IsAttacking = true;
                    _waitAttackin = StartCoroutine(BeforeAttackWait());
                }
                else if (IsAttacking == false)
                {
                    StopCoroutine(AttackCoroutine);
                    _waitAttackin = null;
                    AttackCoroutine = null;
                }
            }
        }
    }

    private IEnumerator BeforeAttackWait()
    {
        yield return _beforeAttackingDelay;

        if (AttackCoroutine == null)
        {
            AttackCoroutine = StartCoroutine(DealAreaDamage());
            MakeDamage?.Invoke();
        }
        StopCoroutine(_waitAttackin);
    }

    private CharacterProperties AcquireTarget()
    {
        CharacterProperties selfTarget = transform.GetComponent<CharacterProperties>();
        float currentSearchDistance = 1f;

        do
        {
            Collider[] targets = Physics.OverlapSphere(transform.position, currentSearchDistance + Scale);

            for (int i = 0; i < targets.Length; i++)
            {
                if (targets[i].TryGetComponent(out CharacterProperties enemy))
                    if (enemy != selfTarget)
                        return enemy;
            }
            currentSearchDistance++;
            currentSearchDistance = Mathf.Clamp(currentSearchDistance, MinSearchDistance, _searchDistance);

        } while (currentSearchDistance < _searchDistance);
        return null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(AttackPoint.position, AttackRange);
    }
}
