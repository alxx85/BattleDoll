using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartWindow : Window
{
    [SerializeField] private Button _start;
    [SerializeField] private Button _settings;

    private EnemySpawner _spawner;
    private SettingsWindow _settingsMenu;

    protected override void OnEnable()
    {
        base.OnEnable();
        _start.onClick.AddListener(OnStartButtonClick);
        _settings.onClick.AddListener(OnSettingsButtonClick);

    }

    protected override void OnDisable()
    {
        base.OnDisable();
        _start.onClick.RemoveListener(OnStartButtonClick);
        _settings.onClick.RemoveListener(OnSettingsButtonClick);
    }

    public void Init(EnemySpawner spawner, PlayerMover player, SettingsWindow settingsMenu)
    {
        _spawner = spawner;
        _player = player;
        _player.enabled = false;
        _settingsMenu = settingsMenu;
    }

    private void OnStartButtonClick()
    {
        _player.enabled = true;
        _spawner.StartBattle();
        GetComponentInParent<ScreenInfoViewer>().PlayingBattleSong();
        CloseWindow();
    }

    private void OnSettingsButtonClick()
    {
        _settingsMenu.Init(_player, this);
        _settingsMenu.gameObject.SetActive(true);
        CloseWindow();
    }

    protected override void OnExitButtonClick()
    {
        Application.Quit();
    }

    private void CloseWindow()
    {
        gameObject.SetActive(false);
    }
}
