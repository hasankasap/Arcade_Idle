using UnityEngine;

[CreateAssetMenu(fileName = "Level", menuName = "ScriptableObjects/Level")]
public class LevelSO : ScriptableObject
{
    public Level LevelPrefab;

    public Level GetLevelPrefab()
    {
        return LevelPrefab;
    }
}