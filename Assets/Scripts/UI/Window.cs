using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Window : MonoBehaviour
{
    [SerializeField] protected Button _exitButton;

    protected PlayerMover _player;

    protected virtual void OnEnable()
    {
        _exitButton.onClick.AddListener(OnExitButtonClick);
    }

    protected virtual void OnDisable()
    {
        _exitButton.onClick.RemoveListener(OnExitButtonClick);
    }

    protected abstract void OnExitButtonClick();
}
