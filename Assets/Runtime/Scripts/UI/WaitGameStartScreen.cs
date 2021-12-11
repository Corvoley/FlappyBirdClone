using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WaitGameStartScreen : MonoBehaviour
{
    [SerializeField] private GameMode gameMode;

    public void OnStartGameClicked()
    {
        gameMode.StartGame();
    }
}
