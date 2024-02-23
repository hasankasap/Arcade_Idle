using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public Level GenerateLevel(Level prefab)
    {
        return Instantiate(prefab);
    }

}