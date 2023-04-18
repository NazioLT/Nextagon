using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Nazio_LT.Tools.Core;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private GameMode currentGameMode;

    public void Play(GameMode _gameMode)
    {
        currentGameMode = _gameMode;
        SceneManager.LoadScene("Game");
    }

    public static GameMode GameMode => instance.currentGameMode;
}