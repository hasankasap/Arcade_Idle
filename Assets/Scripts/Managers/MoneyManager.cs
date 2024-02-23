
using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

namespace Game
{
    public class MoneyManager : BaseSingleton<MoneyManager>
    {
        [SerializeField] private GameInfoSO gameInfoSO;
        void OnEnable()
        {
            EventManager.StartListening(GameEvents.ADD_MONEY, OnMoneyAddEvent);
        }
        void OnDisable()
        {
            EventManager.StopListening(GameEvents.ADD_MONEY, OnMoneyAddEvent);
        }
        public void Initialize()
        {
            float value = gameInfoSO.GetPlayerMoney();
            EventManager.TriggerEvent(GameEvents.UPDATE_TOTAL_MONEY_UI, new object[] { value });
        }
        private void OnMoneyAddEvent(object[] obj)
        {
            int value = (int)obj[0];
            gameInfoSO.AddPlayerMoney(value);
            string moneyText = TextUtility.FloatToStringConverter(value);
            EventManager.TriggerEvent(GameEvents.UPDATE_TOTAL_MONEY_UI, new object[] { moneyText, value });
        }
    }
}