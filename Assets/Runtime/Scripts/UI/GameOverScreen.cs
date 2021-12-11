using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverScreen : MonoBehaviour
{
    [SerializeField] private GameMode gameMode;
    [SerializeField] private MedalRewardCalculator medalRewardCalculator;

    [Space]
    [Header("Elements")]
    [SerializeField] private Image medalImage;
    [SerializeField] private TextMeshProUGUI currentScoreText;
    [SerializeField] private TextMeshProUGUI highScoreText;

    private void OnEnable()
    {
        UpdateUI();

    }

    private void UpdateUI()
    {
        currentScoreText.text = gameMode.Score.ToString();
        highScoreText.text = gameMode.HighScore.ToString();

        Medal medal = medalRewardCalculator.GetMedalForScore(gameMode.Score);
        if (medal !=null)
        {
            medalImage.sprite = medal.MedalSprite;
        }
        else
        {
            medalImage.gameObject.SetActive(false);
        }
    }

    public void OnRetryClicked()
    {
        gameMode.ReloadGame();
    }
    public void OnQuitClicked()
    {
        gameMode.QuitGame();
    }

}
