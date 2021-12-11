using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameHudScreen : MonoBehaviour
{
    [SerializeField] private GameMode gameMode;
    [SerializeField] private TextMeshProUGUI scoreText;

    private void LateUpdate()
    {
        scoreText.text = gameMode.Score.ToString();
    }
    public void OnPauseClicked()
    {
        gameMode.PauseGame();
    }
}
