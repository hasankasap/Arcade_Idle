using UnityEngine;
[CreateAssetMenu(fileName = "SpawnerSO", menuName = "ScriptableObjects/SpawnerSO")]
public class SpawnerSO : ScriptableObject
{
    public float SpawnTimer;
    public GameObject SpawnPrefab;
}