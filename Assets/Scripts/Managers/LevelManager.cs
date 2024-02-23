using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : BaseSingleton<LevelManager>
{
    [SerializeField] private LevelSO[] levelPrefabs;
    private Level currentLevelPrefab;
    private int CurrentLevel = 0;
    [SerializeField] LevelGenerator levelGenerator;

    public void Initialize()
    {
        LoadLevel(CurrentLevel);
    }

    public void SetCurrentLevel(int currentIndex)
    {
        CurrentLevel = currentIndex;
    }

    private void LoadLevel(int index)
    {
        if (levelPrefabs.Length == 0)
        {
            Debug.Log("There is no attached level.");
            return;
        }
        if (index < 0)
        {
            Debug.LogError("Invalid level index: " + index);
            return;
        }
        if (index >= levelPrefabs.Length)
        {
            index %= levelPrefabs.Length;
        }
        // Destroy any existing level
        if (currentLevelPrefab != null)
        {
            Destroy(currentLevelPrefab.gameObject);
        }

        // Instantiate the new level
        currentLevelPrefab = levelGenerator.GenerateLevel(levelPrefabs[index].GetLevelPrefab());
        EventManager.TriggerEvent(GameEvents.LEVEL_LOADED, new object[] { });
    }

    public void LoadSameLevel()
    {
        LoadLevel(CurrentLevel);
    }

    public void LoadNextLevel()
    {
        CurrentLevel++;
        LoadLevel(CurrentLevel);
    }
}

