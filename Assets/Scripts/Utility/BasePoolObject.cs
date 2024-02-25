using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePoolObject<T> : MonoBehaviour
{
    public static BasePoolObject<T> Instance;
    protected Dictionary<string, List<T>> pool = new Dictionary<string, List<T>>();
    public Transform parent;
    protected virtual void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject); // Destroy any additional instances
        }
        ObjectPooling.clearPool += ClearPool;
    }

    protected virtual void BuildDictionary(string key)
    {
        if (pool.ContainsKey(key) == false)
        {
            List<T> temp = new List<T>();
            pool.Add(key, temp);
        }
        if (parent == null)
        {
            parent = new GameObject(key).transform;
        }
    }
    public virtual T GetObjectFromPool(T prefab, string name)
    {
        if (pool.Count == 0 || pool.ContainsKey(name) == false)
        {
            BuildDictionary(name);
        }
        return GetPoolObject(prefab, name);
    }
    protected virtual T GetPoolObject(T prefab, string name)
    {
        var particleCount = pool[name].Count;
        T poolObject;
        if (particleCount > 0)
        {
            poolObject = pool[name][0];
            pool[name].Remove(poolObject);
        }
        else
        {
            if (prefab == null)
            {
                return default(T);
            }
            poolObject = CreateNewObject(prefab, name);
        }
        return poolObject;
    }
    public void ReturnPool(T prefab, string name)
    {
        pool[name].Add(prefab);
    }
    protected virtual T CreateNewObject(T prefab, string name)
    {
        // TODO: this is not working with gameobject fix it
        if (prefab != null)
        {
            Component original = (Component)Convert.ChangeType(prefab, typeof(T));
            Component instanceObject = Instantiate(original);
            instanceObject.gameObject.name = original.gameObject.name;
            instanceObject.transform.parent = parent;
            return (T)Convert.ChangeType(instanceObject, typeof(T));
        }
        return default(T);
    }
    public virtual void ClearPool()
    {
        //if (pool.Count == 0) return;
        //foreach (var pair in pool)
        //{
        //    if (pair.Value.Count == 0) continue;
        //    foreach (T item in pair.Value)
        //    {
        //        if (item is MonoBehaviour monoBehaviour)
        //        {
        //            Destroy(monoBehaviour.gameObject);
        //        }
        //    }
        //    pair.Value.Clear();
        //}
        if (parent != null) Destroy(parent.gameObject);
        pool.Clear();
    }

    protected virtual void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }
}