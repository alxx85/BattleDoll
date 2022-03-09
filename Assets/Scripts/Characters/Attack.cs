using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterProperties))]
public class Attack : MonoBehaviour
{
    [SerializeField] protected float Damage;
    [SerializeField] protected float AttackRange;
    [SerializeField] protected Transform AttackPoint;
    [SerializeField] protected float AttackAnimationsLength;
    [SerializeField] protected GameObject AttackParticte;

    protected float Scale;
    protected float CurrentDamage;
    protected Coroutine AttackCoroutine;
    protected WaitForSeconds DelayAttack;
    protected CharacterProperties Properties;

    public bool IsAttacking { get; protected set; }

    private void Awake()
    {
        Properties = GetComponent<CharacterProperties>();
        IsAttacking = false;
    }

    protected virtual void OnEnable()
    {
        Properties.ScaleChanged += OnScaleChanged;
    }

    protected virtual void OnDisable()
    {
        Properties.ScaleChanged -= OnScaleChanged;
    }

    protected virtual void Start()
    {
        DelayAttack = new WaitForSeconds(AttackAnimationsLength);
        CurrentDamage = Damage;
    }

    public void AddCrashEffect()
    {
        Instantiate(AttackParticte, AttackPoint.position, Quaternion.identity);
    }

    protected void OnScaleChanged(float newScale)
    {
        Scale = newScale;
        CurrentDamage = Damage + Damage * Scale;
    }

    protected IEnumerator DealAreaDamage()
    {
        CharacterProperties selfTarget = Properties;
        yield return DelayAttack;

        Collider[] targets = Physics.OverlapSphere(AttackPoint.position, AttackRange + Scale);

        for (int i = 0; i < targets.Length; i++)
        {
            if (targets[i].TryGetComponent(out CharacterProperties enemy))
            {
                if (enemy != selfTarget)
                {
                    enemy.TakeDamage(GetComponent<CharacterProperties>(), CurrentDamage);
                }
            }
        }
        IsAttacking = false;
    }

}
