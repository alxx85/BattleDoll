using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Window : MonoBehaviour
{
    [SerializeField] protected Button ExitButton;

    protected PlayerMover _player;

    protected virtual void OnEnable()
    {
        ExitButton.onClick.AddListener(OnExitButtonClick);
    }

    protected virtual void OnDisable()
    {
        ExitButton.onClick.RemoveListener(OnExitButtonClick);
    }

    protected abstract void OnExitButtonClick();
}
