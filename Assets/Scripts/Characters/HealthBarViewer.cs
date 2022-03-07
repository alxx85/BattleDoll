using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterProperties))]
public class HealthBarViewer : MonoBehaviour
{
    [SerializeField] private Texture2D _healthBarTexture;

    private CharacterProperties _properties;
    private int _healthBarWidth = 20;
    private int _healthBarHeight = 7;

    private float _maxHealth;
    private float _health;

    private void OnEnable()
    {
        _properties = GetComponent<CharacterProperties>();
        _properties.ChangedHealth += OnChangedHealth;
    }

    private void OnDisable()
    {
        _properties.ChangedHealth -= OnChangedHealth;
    }

    private void OnGUI()
    {
        float healthPercent = _health * 100 / _maxHealth;
        float currentHealth = _healthBarWidth * healthPercent / 100;
        Vector3 healthBarPosition = Camera.main.WorldToScreenPoint(transform.position);
        GUI.Box(new Rect(healthBarPosition.x - _healthBarWidth / 2, Screen.height - healthBarPosition.y + _healthBarHeight,
                _healthBarWidth, _healthBarHeight), "");
        GUI.DrawTexture(new Rect(healthBarPosition.x - _healthBarWidth / 2, Screen.height - healthBarPosition.y + _healthBarHeight,
                currentHealth, _healthBarHeight), _healthBarTexture);
    }

    private void OnChangedHealth(float maxHealth, float health)
    {
        _maxHealth = maxHealth;
        _health = health;
    }
}
