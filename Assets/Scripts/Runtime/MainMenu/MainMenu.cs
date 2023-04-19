using UnityEngine;
using Nazio_LT.Tools.UI;

public class MainMenu : MonoBehaviour
{
    private GameMode gameMode;
    [SerializeField] private Menu menu;

    private void Start()
    {
        gameMode = GameManager.GameMode;
    }

    public void Play()
    {
        GameManager.instance.Play(gameMode);
    }

    public void ChooseGameMode(GameMode _gameMode)
    {
        gameMode = _gameMode;
        menu.SwitchPanel("Powers");
    }
}
