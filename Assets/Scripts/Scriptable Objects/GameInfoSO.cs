using UnityEngine;
[CreateAssetMenu(fileName = "GameInfoSO", menuName = "ScriptableObjects/GameInfoSO")]

[System.Serializable]
public class GameInfoSO : DataSO
{
    public GameInfoData data = new GameInfoData();

    public override void ResetData(DataBaseManager dataBase = null)
    {
        data.levelData = 0;
        data.playerMoney = 0;
        base.ResetData(dataBase);
    }
    public void AddPlayerMoney(int value)
    {
        data.playerMoney += value;
        SaveData();
    }
    public int GetPlayerMoney()
    {
        return data.playerMoney;
    }

    public override void SaveData(DataBaseManager dataBase = null)
    {
        base.SaveData(dataBase);
        dataBase = CheckDatabase(dataBase);
        if (dataBase == null) return;
        dataBase.Save(data, this.name);
    }
    public override void LoadData(DataBaseManager dataBase = null)
    {
        base.LoadData(dataBase);
        dataBase = CheckDatabase(dataBase);
        if (dataBase == null) return;
        data = dataBase.Load(data, this.name);
    }
    public int GetLevelData()
    {
        return data.levelData;
    }
    public void IncreaseLevelData()
    {
        data.levelData++;
        SaveData();
    }
}
[System.Serializable]
public class GameInfoData
{
    public int playerMoney;
    public int levelData;
}