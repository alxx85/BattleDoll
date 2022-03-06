using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndGameWindow : Window
{
    [SerializeField] private Button _replay;
    [SerializeField] private TMP_Text _infoText;

    protected override void OnEnable()
    {
        base.OnEnable();
        _replay.onClick.AddListener(OnNextButtonClick);

    }

    protected override void OnDisable()
    {
        base.OnDisable();
        _replay.onClick.RemoveListener(OnNextButtonClick);
    }

    private void OnNextButtonClick()
    {
        SceneManager.LoadScene(0);
    }

    protected override void OnExitButtonClick()
    {
        Application.Quit();
    }

    public void ShowInfoText(int battleCount, int killingEnemy)
    {
        string showString = $"Current battle: {battleCount}\nEnemy killing: {killingEnemy}";
        _infoText.text = showString;
        ChangeLeadre(battleCount, killingEnemy);
    }

    private void ChangeLeadre(int battleCount, int killing)
    {
        if (PlayerPrefs.HasKey("Battles"))
        {
            int leaderBattlesCount = PlayerPrefs.GetInt("Battles");
            
            if (leaderBattlesCount < battleCount)
            {
                PlayerPrefs.SetInt("Battles", battleCount);
                PlayerPrefs.SetInt("Killing", killing);
                PlayerPrefs.Save();
            }
        }
    }
}
