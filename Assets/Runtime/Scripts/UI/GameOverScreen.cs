using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
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

    [Space]
    [Header("Containers")]
    [SerializeField] private CanvasGroup scoreBoardContanier;
    [SerializeField] private CanvasGroup gameOverImageContanier;
    [SerializeField] private CanvasGroup buttonsContanier;

    [Space]
    [Header("Score Board Tween")]
    [SerializeField] private float scoreBoardAnimationTime = 0.5f;
    [SerializeField] private Transform scoreBoardStart;
    [SerializeField] private float scoreTweenDelaySeconds = 0.2f;

    [Space]
    [Header("Game Over Tween")]
    [SerializeField] private Transform gameOverStart;
    [SerializeField] private float gameOverTweenTimeSeconds;

    [Space]
    [Header("Buttons Tween")]
    [SerializeField] private float buttonsTweenDelaySeconds = 0.5f;
    [SerializeField] private float buttonsTweenTimeSeconds = 0.5f;



    private void OnEnable()
    {
        UpdateUI();
        StartCoroutine(Show());

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
    private IEnumerator Show()
    {
        gameOverImageContanier.alpha = 0;
        gameOverImageContanier.blocksRaycasts = false;

        scoreBoardContanier.alpha = 0;
        scoreBoardContanier.blocksRaycasts = false;

        buttonsContanier.alpha = 0;
        buttonsContanier.blocksRaycasts = false;

        yield return StartCoroutine(
            AnimateCanvasGroup(
                gameOverImageContanier,
                gameOverStart.position,
                gameOverImageContanier.transform.position,
                gameOverTweenTimeSeconds));

        yield return new WaitForSeconds(scoreTweenDelaySeconds);

        yield return StartCoroutine(
            AnimateCanvasGroup(
                scoreBoardContanier,
                scoreBoardStart.position,
                scoreBoardContanier.transform.position,
                scoreBoardAnimationTime));

        yield return new WaitForSeconds(buttonsTweenDelaySeconds);

        yield return StartCoroutine(
            AnimateCanvasGroup(
                buttonsContanier,
                buttonsContanier.transform.position,
                buttonsContanier.transform.position,
                buttonsTweenTimeSeconds));
    }
    private IEnumerator AnimateCanvasGroup(CanvasGroup group, Vector3 from, Vector3 to, float time)
    {
        group.alpha = 0;
        group.blocksRaycasts = false;
        Tween fadeTween = group.DOFade(1, time);
        group.transform.position = from;
        Tween transformTween = group.transform.DOMove(to, time);

        yield return fadeTween.WaitForKill();
        group.blocksRaycasts = true;
    }

}
