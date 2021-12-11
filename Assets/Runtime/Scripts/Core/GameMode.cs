using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class GameMode : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;
    [SerializeField] private EndlessPipeGenerator pipeGenerator;
    [SerializeField] private ScreenController screenController;
    [SerializeField] private GameSaver gameSaver;

    [Header("Data")]
    [SerializeField] private PlayerMovementParameters gameRunningParameters;
    [SerializeField] private PlayerMovementParameters gameOverParameters;
    [SerializeField] private PlayerMovementParameters waitGameStartParameters;
    [SerializeField] private PlayerMovementParameters stopAllMovimentParameters;

    public int Score { get; private set; }
    public int HighScore => gameSaver.CurrentSave.HighestScore < Score ? Score : gameSaver.CurrentSave.HighestScore;


    private void Awake()
    {
        gameSaver.LoadGame();
        WaitGameStart();
    }

    public void WaitGameStart()
    {
        playerController.MovementParameters = waitGameStartParameters;
        screenController.ShowWaitToStartGameScreen();
    }
    public void StartGame()
    {
        playerController.MovementParameters = gameRunningParameters;
        playerController.Flap();        
        pipeGenerator.StartPipeSpawn();
        screenController.ShowInGameHud();

    }
    public void PauseGame()
    {
        Time.timeScale = 0;
        screenController.ShowPauseScreen();
    }
    public void ResumeGame()
    {
        Time.timeScale = 1;
        screenController.ShowInGameHud();
    }

    public void GameOver()
    {
        playerController.MovementParameters = gameOverParameters;
        gameSaver.SaveGame(new SaveGameData { HighestScore = HighScore });
        screenController.ShowGameOverScreen();
    }
    public void ReloadGame()
    {                    
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);        
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
    public void IncrementScore()
    {
        Score++;
    }

    public void StopAllMoviment()
    {
        playerController.MovementParameters = stopAllMovimentParameters;
    }
}
