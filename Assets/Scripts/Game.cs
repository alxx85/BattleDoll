using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] private CharacterProperties _player;
    [SerializeField] private EnemySpawner _spawner;
    [SerializeField] private ScreenInfoViewer _infoViewer;

    private Camera _camera;

    public static Game Instance = null;

    public int BattleCount { get; private set; }

    private void Awake()
    {
        if (Game.Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Game.Instance = this;

        _camera = Camera.main;
        ChangeCameraParent(_player.transform);

        BattleCount = 0;
        _infoViewer.ShowStartMenu();
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

    public void StartBattle()
    {
        _spawner.SpawnEnemy();
    }

    private void OnEndedBattle()
    {
        BattleCount++;
        _player.ResetProperties();
        _infoViewer.ShowEndBattleMenu(BattleCount, _player.Killing);
    }

    private void OnPlayerDying(CharacterProperties player)
    {
        _player.Dying -= OnPlayerDying;
        ChangeCameraParent(null);
        _infoViewer.ShowEndGameMenu(BattleCount, _player.Killing);
        _player = null;
    }

    private void ChangeCameraParent(Transform parent)
    {
        _camera.transform.SetParent(parent);
    }
}
