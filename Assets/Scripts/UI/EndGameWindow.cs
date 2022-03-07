using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndGameWindow : Window
{
    [SerializeField] private Button _replayButton;
    [SerializeField] private TMP_Text _infoText;

    private const string Battles = "Battles";
    private const string Killing = "Killing";

    protected override void OnEnable()
    {
        base.OnEnable();
        _replayButton.onClick.AddListener(OnNextButtonClick);

    }

    protected override void OnDisable()
    {
        base.OnDisable();
        _replayButton.onClick.RemoveListener(OnNextButtonClick);
    }

    public void ShowInfoText(int battleCount, int killingEnemy)
    {
        string showString = $"Current battle: {battleCount}\nEnemy killing: {killingEnemy}";
        _infoText.text = showString;
        ChangeLeadre(battleCount, killingEnemy);
    }

    protected override void OnExitButtonClick()
    {
        Application.Quit();
    }

    private void OnNextButtonClick()
    {
        SceneManager.LoadScene(0);
    }

    private void ChangeLeadre(int battleCount, int killing)
    {
        if (PlayerPrefs.HasKey(Battles))
        {
            int leaderBattlesCount = PlayerPrefs.GetInt(Battles);
            
            if (leaderBattlesCount < battleCount)
            {
                PlayerPrefs.SetInt(Battles, battleCount);
                PlayerPrefs.SetInt(Killing, killing);
            }
        }
        else
        {
            PlayerPrefs.SetInt(Battles, battleCount);
            PlayerPrefs.SetInt(Killing, killing);
        }
        PlayerPrefs.Save();
    }
}
