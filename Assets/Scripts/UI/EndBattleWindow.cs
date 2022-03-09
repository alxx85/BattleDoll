using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndBattleWindow : Window
{
    [SerializeField] private Button _nextButton;
    [SerializeField] private TMP_Text _infoText;

    protected override void OnEnable()
    {
        base.OnEnable();
        _nextButton.onClick.AddListener(OnNextButtonClick);

    }

    protected override void OnDisable()
    {
        base.OnDisable();
        _nextButton.onClick.RemoveListener(OnNextButtonClick);
    }

    public void Init(CharacterProperties player)
    {
        _player = player.GetComponent<PlayerMover>();
        _player.enabled = false;
    }

    private void OnNextButtonClick()
    {
        Game.Instance.StartBattle();
        gameObject.SetActive(false);
        _player.enabled = true;
    }

    protected override void OnExitButtonClick()
    {
        SceneManager.LoadScene(0);
    }

    public void ShowInfoText(int battleCount, int killedEnemysCount)
    {
        string showString = $"Current battle: {battleCount}\nEnemy killing: {killedEnemysCount}";
        _infoText.text = showString;
    }
}
