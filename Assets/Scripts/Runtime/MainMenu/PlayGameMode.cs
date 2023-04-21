using UnityEngine;
using Nazio_LT.Tools.UI;

public class PlayGameMode : MonoBehaviour
{
    [SerializeField] private GameMode gameMode;
    [SerializeField] private NButton button;
    [SerializeField] private MainMenu mainMenu;

    private void Awake()
    {
        if (button == null)
        {
            Destroy(this);
            return;
        }

        button.onClick.AddListener(() =>
        {
            mainMenu.ChooseGameMode(gameMode);
        });
    }
}
