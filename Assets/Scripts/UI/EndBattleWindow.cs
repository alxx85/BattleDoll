using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndBattleWindow : Window
{
    [SerializeField] private Button _next;
    [SerializeField] private TMP_Text _infoText;

    private EnemySpawner _spawner;

    protected override void OnEnable()
    {
        base.OnEnable();
        _next.onClick.AddListener(OnNextButtonClick);

    }

    protected override void OnDisable()
    {
        base.OnDisable();
        _next.onClick.RemoveListener(OnNextButtonClick);
    }

    public void Init(EnemySpawner spawner, CharacterProperties player)
    {
        _spawner = spawner;
        player.NextBattle();
        _player = player.GetComponent<PlayerMover>();
        _player.enabled = false;
    }

    private void OnNextButtonClick()
    {
        _spawner.StartBattle();
        gameObject.SetActive(false);
        _player.enabled = true;
    }

    protected override void OnExitButtonClick()
    {
        SceneManager.LoadScene(0);
    }

    public void ShowInfoText(int battleCount, int killingEnemy)
    {
        string showString = $"Current battle: {battleCount}\nEnemy killing: {killingEnemy}";
        _infoText.text = showString;
    }
}
