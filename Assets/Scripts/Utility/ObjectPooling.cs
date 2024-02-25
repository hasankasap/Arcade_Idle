using System;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooling : MonoBehaviour
{
    public static Action ClearPoolAction;
    void Awake()
    {
        gameObject.AddComponent<ProductPool>();
        gameObject.AddComponent<GameObjPool>();
        
    }
}
public class ProductPool : BasePoolObject<Product>
{
}
public class GameObjPool : BasePoolObject<GameObject>
{
    // You can add additional methods or properties specific to GameObjPool here
}
