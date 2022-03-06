using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenInfoViewer : MonoBehaviour
{
    [SerializeField] private CharacterProperties _player;
    [SerializeField] private Slider _healthBar;
    [SerializeField] private StartWindow _startMenu;
    [SerializeField] private SettingsWindow _settingsMenu;
    [SerializeField] private EndBattleWindow _endBattle;
    [SerializeField] private EndGameWindow _endGame;
    [Space]
    [SerializeField] private AudioSource _menuSong;
    [SerializeField] private AudioSource _battleSong;

    private void Start()
    {
        _startMenu.gameObject.SetActive(true);
    }

    private void OnEnable()
    {
        _player.ChangeHealth += OnChangeHealth;
    }

    private void OnDisable()
    {
        _player.ChangeHealth -= OnChangeHealth;
    }

    public void PlayingBattleSong()
    {
         _menuSong.Stop();
        _battleSong.Play();
    }

    public void ShowEndGameMenu(int BattleCount, int enemyKilling)
    {
        _endGame.ShowInfoText(BattleCount, enemyKilling);
        _endGame.gameObject.SetActive(true);
    }

    public void ShowStartMenu(EnemySpawner spawner)
    {
        _startMenu.Init(spawner, _player.GetComponent<PlayerMover>(), _settingsMenu);
        _startMenu.gameObject.SetActive(true);
    }
    
    public void ShowEndBattleMenu(EnemySpawner spawner, int BattleCount, int enemyKilling)
    {
        _endBattle.Init(spawner, _player);
        _endBattle.ShowInfoText(BattleCount, enemyKilling);
        _endBattle.gameObject.SetActive(true);
    }

    private void OnChangeHealth(float maxHealth, float currentHealth)
    {
        _healthBar.maxValue = maxHealth;
        _healthBar.value = currentHealth;
    }
}
