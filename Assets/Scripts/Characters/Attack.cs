using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterProperties))]
public class Attack : MonoBehaviour
{
    [SerializeField] protected float _damage;
    [SerializeField] protected float _attackRange;
    [SerializeField] protected Transform _attackPoint;
    [SerializeField] protected float _attackAnimationsLength;
    [SerializeField] protected GameObject _attackParticte;

    protected float _scale;
    protected float _currentDamage;
    protected Coroutine _attack;
    protected WaitForSeconds _delayAttack;
    protected bool _isAttacking = false;

    public bool IsAttacking => _isAttacking;

    protected virtual void OnEnable()
    {
        GetComponent<CharacterProperties>().ScaleChanged += OnScaleChanged;
    }

    protected virtual void OnDisable()
    {
        GetComponent<CharacterProperties>().ScaleChanged -= OnScaleChanged;
    }

    protected virtual void Start()
    {
        _delayAttack = new WaitForSeconds(_attackAnimationsLength);
        _currentDamage = _damage;
    }

    public void AddingCrashEffect()
    {
        Instantiate(_attackParticte, _attackPoint.position, Quaternion.identity);
    }

    protected void OnScaleChanged(float newScale)
    {
        _scale = newScale;
        _currentDamage = _damage + _damage * _scale;
    }

    protected IEnumerator DealAreaDamage()
    {
        CharacterProperties selfTarget = GetComponent<CharacterProperties>();
        yield return _delayAttack;

        Collider[] targets = Physics.OverlapSphere(_attackPoint.position, _attackRange + _scale);

        for (int i = 0; i < targets.Length; i++)
        {
            if (targets[i].TryGetComponent(out CharacterProperties enemy))
            {
                if (enemy != selfTarget)
                {
                    enemy.TakeDamage(GetComponent<CharacterProperties>(), _currentDamage);
                }
            }
        }
        _isAttacking = false;
    }

}
