using KG.Viewports;
using System;
using GameProject.Game;
using UnityEngine;

public class LevelHandler: IDisposable
{
    public event Action OnStartLevel;
    public event Action OnResetLevel;
    public event Action OnLoseLevel;

    public LevelState StateLevel { get; private set; }

    public LevelHandler()
    {
      
    }
    public void Dispose()
    {

    }

    public void StartLevel()
    {
        StateLevel = LevelState.Gaming;
        OnStartLevel?.Invoke();
    }

    public void Restart()
    {
        StateLevel = LevelState.Prepare;
        OnResetLevel?.Invoke();
    }

    public void SetGameOver()
    {
        StateLevel = LevelState.Lose;
        OnLoseLevel?.Invoke();
    }
}