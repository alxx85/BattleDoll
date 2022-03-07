using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] private CharacterProperties _player;
    [SerializeField] private EnemySpawner _spawner;
    [SerializeField] private ScreenInfoViewer _infoViewer;

    public int BattleCount { get; private set; }

    private void Start()
    {
        BattleCount = 0;
        _infoViewer.ShowStartMenu(_spawner);
    }

    private void OnEnable()
    {
        _player.Dying += OnPlayerDying;
        _spawner.EndedBattle += OnEndedBattle;
    }

    private void OnDisable()
    {
        _spawner.EndedBattle -= OnEndedBattle;
        if (_player != null)
            _player.Dying -= OnPlayerDying;
    }

    private void OnEndedBattle()
    {
        BattleCount++;
        _infoViewer.ShowEndBattleMenu(_spawner, BattleCount, _player.Killing);
    }

    private void OnPlayerDying(CharacterProperties player)
    {
        _player.Dying -= OnPlayerDying;
        _infoViewer.ShowEndGameMenu(BattleCount, _player.Killing);
        _player = null;
    }
}
