using System.Collections;
using UnityEngine;
using Nazio_LT.Tools.Core;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private GameMode currentGameMode;

    public void SelectGameMode()
    {

    }

    public static GameMode GameMode => instance.currentGameMode;
}