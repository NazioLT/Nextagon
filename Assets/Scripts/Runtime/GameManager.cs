using UnityEngine;
using UnityEngine.SceneManagement;
using Nazio_LT.Tools.Core;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private GameMode currentGameMode;
    private Powers[] powers = { global::Powers.Jump, global::Powers.Clean, global::Powers.GrowUp };

    public void Play(GameMode _gameMode, Powers power1, Powers power2, Powers power3)
    {
        powers = new Powers[] { power1, power2, power3 };
        currentGameMode = _gameMode;
        SceneManager.LoadScene("Game");
    }

    public static GameMode GameMode => instance.currentGameMode;
    public static Powers[] Powers => instance.powers;
}