using Game;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : BaseSingleton<GameManager>
{
    [SerializeField] private LevelManager levelManager;
    [SerializeField] private DataBaseManager dataBaseManager;
    [SerializeField] private MoneyManager moneyManager;

    [SerializeField] private GameInfoSO gameInfoSO;

    void Start()
    {
        Initialize();
    }

    void Initialize()
    {
        dataBaseManager.LoadData();
        if (gameInfoSO != null)
        {
            levelManager.SetCurrentLevel(gameInfoSO.GetLevelData());
            levelManager.Initialize();
            moneyManager.Initialize();
        }
    }

    public void OnNextLevelAction()
    {
        gameInfoSO.IncreaseLevelData();
        levelManager.LoadNextLevel();
        if (gameInfoSO != null)
        {
            moneyManager.Initialize();
        }
    }
    public void OnRetryAction()
    {
        dataBaseManager.SaveData();
        levelManager.LoadSameLevel();
    }

}

