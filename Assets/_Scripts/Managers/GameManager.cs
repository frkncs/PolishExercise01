using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameStates
{
    Started,
    Lose
}

public class GameManager : MonoBehaviour
{
    #region Variables
    
    // Public Variables
    public static GameManager Instance;
    public static Action LoseGame;
    public GameStates CurrentGameState { get; private set; }

    // Private Variables

    #endregion Variables

    private void Awake()
    {
        Instance = this;
        CurrentGameState = GameStates.Started;
        
        LoseGame += () =>
        {
            CurrentGameState = GameStates.Lose;
        };
    }

    private void OnDestroy()
    {
        LoseGame = null;
    }
}
