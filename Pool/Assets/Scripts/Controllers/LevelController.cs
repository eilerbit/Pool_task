using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;
using System;

public class LevelController : INotifier
{  
    public LevelController(LevelConfig levelConfig)
    {
        this.LevelConfig = levelConfig;         
    }       

    public int CurrentLevel;

    public LevelConfig LevelConfig;

    public event Action<string> LevelChanged;
    public event Action<string> Notification;

    private IManager manager;

    private float cachedSpeed;
    private int cachedDefenderQuantity;

    private readonly string speedUpNotification = "Speed Up!";
    private readonly string defenderQuantityNotification = "New defender is coming!";
    private readonly string endGameWinNotification = "Game over! You won!";
    private readonly string endGameLooseNotification = "Game over! You lost!";

    public bool UpdateLevel()
    {
        CurrentLevel++;

        if(TryEndGame(LevelConfig.Levels.Length)) return false;

        CheckNewDefenders(LevelConfig.Levels[CurrentLevel - 1].DefenderData.DefenderQuantity);

        CheckSpeedUp(LevelConfig.Levels[CurrentLevel - 1].DefenderData.DefenderSpeed);                

        LevelChanged?.Invoke(CurrentLevel.ToString());

        return true;
    }
       
    public ref TargetData GetTargetsData()
    {
        return ref LevelConfig.Levels[CurrentLevel - 1].TargetData;
    }

    public ref DefenderData GetDefendersData()
    {
        return ref LevelConfig.Levels[CurrentLevel - 1].DefenderData;
    }

    public void OnLooseGame()
    {
        Notification?.Invoke(endGameLooseNotification);
    }

    private bool TryEndGame(int levelQuantity)
    {
        bool endGame = CurrentLevel > levelQuantity;

        if (endGame)
        {            
            Notification?.Invoke(endGameWinNotification);
        }

        return endGame;
    }

    private void CheckNewDefenders(int defendersQuantity)
    {
        if (defendersQuantity > cachedDefenderQuantity)
        {
            cachedDefenderQuantity = LevelConfig.Levels[CurrentLevel - 1].DefenderData.DefenderQuantity;

            Notification?.Invoke(defenderQuantityNotification);
        }
    }

    private void CheckSpeedUp(float defenderSpeed)
    {
        if (defenderSpeed > cachedSpeed)
        {
            cachedSpeed = LevelConfig.Levels[CurrentLevel - 1].DefenderData.DefenderSpeed;

            Notification?.Invoke(speedUpNotification);
        }
    }
}
