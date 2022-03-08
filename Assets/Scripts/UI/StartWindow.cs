using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StartWindow : Window
{
    [SerializeField] private Button _startButton;
    [SerializeField] private Button _settingsButton;
    [SerializeField] private TMP_Text _infoText;

    private SettingsWindow _settingsWindow;

    private const string Battles = "Battles";
    private const string Killing = "Killing";

    protected override void OnEnable()
    {
        base.OnEnable();
        _startButton.onClick.AddListener(OnStartButtonClick);
        _settingsButton.onClick.AddListener(OnSettingsButtonClick);

    }

    protected override void OnDisable()
    {
        base.OnDisable();
        _startButton.onClick.RemoveListener(OnStartButtonClick);
        _settingsButton.onClick.RemoveListener(OnSettingsButtonClick);
    }

    public void Init(PlayerMover player, SettingsWindow settingsMenu)
    {
        _player = player;
        _settingsWindow = settingsMenu;
        _player.enabled = false;
        ShowLeadre();
    }

    private void OnStartButtonClick()
    {
        _player.enabled = true;
        Game.Instance.StartBattle();
        GetComponentInParent<ScreenInfoViewer>().PlayingBattleSong();
        CloseWindow();
    }

    private void OnSettingsButtonClick()
    {
        _settingsWindow.Init(_player, this);
        _settingsWindow.gameObject.SetActive(true);
        CloseWindow();
    }

    protected override void OnExitButtonClick()
    {
        Application.Quit();
    }

    private void ShowLeadre()
    {
        int leaderBattlesCount = 0;
        int killEnemiesCount = 0;

        if (PlayerPrefs.HasKey(Battles))
        {
            leaderBattlesCount = PlayerPrefs.GetInt(Battles);
            killEnemiesCount = PlayerPrefs.GetInt(Killing);
        }
        string showString = $"Current battle: {leaderBattlesCount}\nEnemy killing: {killEnemiesCount}";
        _infoText.text = showString;
    }

    private void CloseWindow()
    {
        gameObject.SetActive(false);
    }
}
