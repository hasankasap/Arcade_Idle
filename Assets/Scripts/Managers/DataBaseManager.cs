using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class DataBaseManager : MonoBehaviour
{
    public static DataBaseManager instance { get; private set; }
    public DataSO[] datas;
    public bool disableDataLoad;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }

    public void Save<T>(T Data, string name)
    {
        try
        {

            string filePath;

#if UNITY_EDITOR
            filePath = Application.dataPath + name + ".json";
#else
                filePath = Path.Combine(Application.persistentDataPath, name + ".json");
        
#endif
            string json = JsonUtility.ToJson(Data);
            File.WriteAllText(filePath, json);
        }
        catch (System.Exception ex)
        {
            Debug.LogError("Error: " + ex.Message);
        }
    }
    public T Load<T>(T Data, string name)
    {
#if UNITY_EDITOR
        if (disableDataLoad) return Data;
#endif
        string filePath;

#if UNITY_EDITOR
        filePath = Application.dataPath + name + ".json";
#else
                filePath = Path.Combine(Application.persistentDataPath, name + ".json");
        
#endif

        if (!File.Exists(filePath))
            return Data;

        string jsonData = File.ReadAllText(filePath);
        Data = JsonUtility.FromJson<T>(jsonData);
        return Data;
    }

    public void SaveData(DataBaseManager dataBase = null)
    {
        if (datas.Length == 0)
            return;
        if (dataBase == null) dataBase = this;
        foreach (DataSO data in datas)
        {
            if (data != null)
                data.SaveData(dataBase);
        }
    }
    public void LoadData(DataBaseManager dataBase = null)
    {
        if (datas.Length == 0)
            return;
        if (dataBase == null) dataBase = this;
        foreach (DataSO data in datas)
        {
            if (data != null)
                data.LoadData(dataBase);
        }
    }
    public void ResetData(DataBaseManager dataBase = null)
    {
        if (datas.Length == 0)
            return;
        if (dataBase == null) dataBase = this;
        for (int i = 0; i < datas.Length; i++)
        {
            if (datas[i] != null)
                datas[i].ResetData(dataBase);
        }
    }
#if UNITY_EDITOR
    public class CustomMenuItems
    {
        // Custom menu item that shows a "Reset All Data" button
        [MenuItem("DataBase/Reset All Data")]
        private static void ResetAllData()
        {
            // Find the DataBaseManager object in the scene
            PlayerPrefs.DeleteAll();
            DataBaseManager manager = FindObjectOfType<DataBaseManager>();

            // If the manager is found, call its ResetAllData method
            if (manager != null)
            {
                manager.ResetData(manager);
            }
            else
            {
                Debug.LogWarning("No DataBaseManager found in the scene.");
            }
        }
        [MenuItem("DataBase/Save All Data")]
        private static void SaveAllData()
        {
            // Find the DataBaseManager object in the scene
            DataBaseManager manager = FindObjectOfType<DataBaseManager>();

            // If the manager is found, call its ResetAllData method
            if (manager != null)
            {
                manager.SaveData(manager);
            }
            else
            {
                Debug.LogWarning("No DataBaseManager found in the scene.");
            }
        }
        [MenuItem("DataBase/Load All Data")]
        private static void LoadAllData()
        {
            // Find the DataBaseManager object in the scene
            DataBaseManager manager = FindObjectOfType<DataBaseManager>();

            // If the manager is found, call its ResetAllData method
            if (manager != null)
            {
                manager.LoadData(manager);
            }
            else
            {
                Debug.LogWarning("No DataBaseManager found in the scene.");
            }
        }
    }
#endif
}
