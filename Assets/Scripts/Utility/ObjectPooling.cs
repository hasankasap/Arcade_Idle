using System;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooling : MonoBehaviour
{
    public static Action clearPool;
    void Awake()
    {
        gameObject.AddComponent<ProductPool>();
    }
    //private void OnEnable()
    //{
    //    EventManager.StartListening(GameEvents.REFRESH_GAME, OnGameRefresh);
    //}
    //private void OnDisable()
    //{
    //    EventManager.StopListening(GameEvents.REFRESH_GAME, OnGameRefresh);   
    //}

    //private void OnGameRefresh(object[] obj)
    //{
    //    clearPool?.Invoke();
    //}
}
public class ProductPool : BasePoolObject<Product>
{
}