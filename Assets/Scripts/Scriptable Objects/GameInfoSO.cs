using UnityEngine;
[CreateAssetMenu(fileName = "GameInfoSO", menuName = "ScriptableObjects/GameInfoSO")]

[System.Serializable]
public class GameInfoSO : DataSO
{
    public GameInfoData Data = new GameInfoData();

    public override void ResetData(DataBaseManager dataBase = null)
    {
        Data.LevelData = 0;
        Data.PlayerMoney = 0;
        base.ResetData(dataBase);
    }
    public void AddPlayerMoney(int value)
    {
        Data.PlayerMoney += value;
        SaveData();
    }
    public int GetPlayerMoney()
    {
        return Data.PlayerMoney;
    }

    public override void SaveData(DataBaseManager dataBase = null)
    {
        base.SaveData(dataBase);
        dataBase = CheckDatabase(dataBase);
        if (dataBase == null) return;
        dataBase.Save(Data, this.name);
    }
    public override void LoadData(DataBaseManager dataBase = null)
    {
        base.LoadData(dataBase);
        dataBase = CheckDatabase(dataBase);
        if (dataBase == null) return;
        Data = dataBase.Load(Data, this.name);
    }
    public int GetLevelData()
    {
        return Data.LevelData;
    }
    public void IncreaseLevelData()
    {
        Data.LevelData++;
        SaveData();
    }
}
[System.Serializable]
public class GameInfoData
{
    public int PlayerMoney;
    public int LevelData;
}