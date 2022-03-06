using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Window : MonoBehaviour
{
    [SerializeField] protected Button _exit;

    protected PlayerMover _player;

    protected virtual void OnEnable()
    {
        _exit.onClick.AddListener(OnExitButtonClick);
    }

    protected virtual void OnDisable()
    {
        _exit.onClick.RemoveListener(OnExitButtonClick);
    }

    protected abstract void OnExitButtonClick();
}
