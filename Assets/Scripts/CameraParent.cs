using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraParent : MonoBehaviour
{
    private CharacterProperties _playerScaling;

    private void Awake()
    {
        _playerScaling = GetComponentInParent<CharacterProperties>();
    }

    private void OnEnable()
    {
        _playerScaling.Dying += OnDying;
    }

    private void OnDisable()
    {
        _playerScaling.Dying -= OnDying;
    }

    private void OnDying(CharacterProperties player)
    {
        transform.parent = null;
        Destroy(this);
    }
}
