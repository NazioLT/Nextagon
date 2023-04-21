using UnityEngine;
using UnityEngine.SceneManagement;
using Nazio_LT.Tools.Core;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private GameMode currentGameMode;
    private Powers[] powers = { global::Powers.Jump, global::Powers.Clean, global::Powers.GrowUp };

    public void Play(GameMode _gameMode, Powers[] _powers)
    {
        if(_powers.Length > 3) throw new System.Exception("Mode than 3 powers has been choosen!");

        powers = _powers;
        currentGameMode = _gameMode;
        SceneManager.LoadScene("Game");
    }

    public static GameMode GameMode => instance.currentGameMode;
    public static Powers[] Powers => instance.powers;
}