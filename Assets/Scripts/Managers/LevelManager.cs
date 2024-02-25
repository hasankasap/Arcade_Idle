using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : BaseSingleton<LevelManager>
{
    [SerializeField] private LevelSO[] levelPrefabs;
    [SerializeField] private LevelGenerator levelGenerator;
    private Level currentLevelPrefab;
    private int currentLevel = 0;

    public void Initialize()
    {
        LoadLevel(currentLevel);
    }

    public void SetCurrentLevel(int currentIndex)
    {
        currentLevel = currentIndex;
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
        LoadLevel(currentLevel);
    }

    public void LoadNextLevel()
    {
        currentLevel++;
        LoadLevel(currentLevel);
    }
}

