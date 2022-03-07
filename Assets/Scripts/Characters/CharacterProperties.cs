using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CharacterProperties : MonoBehaviour
{
    [SerializeField] private float _maxHealth;
    [SerializeField] private float _addScaleBonus;

    private float _health;
    private float _scale = 0;
    private int _killingEnemy = 0;
    private Vector3 _startScale;

    public int Killing => _killingEnemy;

    public event UnityAction<float, float> ChangedHealth;
    public event UnityAction<float> ScaleChanged;
    public event UnityAction<CharacterProperties> Dying;

    private void Start()
    {
        _startScale = transform.localScale;
        _health = _maxHealth;
        ChangedHealth?.Invoke(_maxHealth, _health);
    }

    public void TakeDamage(CharacterProperties aggressor, float damage)
    {
        _health -= damage;

        if (_health <= 0)
        {
            if (_scale == 0)
                aggressor.ChangeScale(_addScaleBonus);
            else
                aggressor.ChangeScale(_scale + _addScaleBonus);

            Dying?.Invoke(this);
            Destroy(gameObject);
        }
        ChangedHealth?.Invoke(_maxHealth, _health);
    }

    public void ChangeScale(float addScale)
    {
        _scale += addScale;
        ScaleChanged?.Invoke(_scale);
        transform.localScale = _startScale + new Vector3(_scale, _scale, _scale);
        _killingEnemy++;
    }

    public void NextBattle()
    {
        transform.localScale = _startScale;
        transform.position = Vector3.zero;
        _scale = 0;
        ScaleChanged?.Invoke(_scale);
    }
}
