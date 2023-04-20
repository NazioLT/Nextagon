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

    public void Play(Powers[] _powers)
    {
        GameManager.instance.Play(gameMode, _powers);
    }

    public void ChooseGameMode(GameMode _gameMode)
    {
        gameMode = _gameMode;
        menu.SwitchPanel("Powers");
    }
}
