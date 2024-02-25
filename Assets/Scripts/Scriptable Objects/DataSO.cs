using UnityEngine;
[CreateAssetMenu(fileName = "DataSO", menuName = "ScriptableObjects/DataSO")]

[System.Serializable]
public class DataSO : ScriptableObject
{
    public virtual void SaveData(DataBaseManager dataBase = null)
    {
    }
    public virtual void LoadData(DataBaseManager dataBase = null)
    {
    }
    public virtual void ResetData(DataBaseManager dataBase = null)
    {
        dataBase = CheckDatabase(dataBase);
        if (dataBase == null) return;
        SaveData(dataBase);
    }
    protected virtual DataBaseManager CheckDatabase(DataBaseManager dataBase)
    {
        if (dataBase == null)
        {
            dataBase = DataBaseManager.Instance;
        }
        if (dataBase == null)
        {
            dataBase = FindObjectOfType<DataBaseManager>();
        }
        if (dataBase == null)
        {
            Debug.LogWarning("No DataBaseManager found in the scene.");
            return null;
        }
        return dataBase;
    }
}