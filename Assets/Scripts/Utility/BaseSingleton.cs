using System.Threading;
using UnityEngine;

// This object's lifetime depends on the related Scene. When the scene destroyed, this object is destroyed.
public class BaseSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;
    private static bool isDestroyed;

    public static T Instance
    {
        get
        {
            if (isDestroyed)
            {
                Debug.LogWarning($"[Singleton] Instance '{typeof(T).FullName}' already destroyed. Returning null.");
                return null;
            }
            if (instance == null)
            {
                instance = (T)FindObjectOfType(typeof(T));
                if (instance == null)
                {
                    Debug.Log(typeof(T) + " singleton object is not instantiated yet.");
                }

                if (FindObjectsOfType(typeof(T)).Length > 1)
                {
                    Debug.LogError("[Singleton] Something went really wrong - there should never be more then 1 singletion!" +
                            " Reopening the scene might fix it. Thread: " + Thread.CurrentThread.Name);
                }
            }
            return instance;
        }
    }

    virtual protected void Awake()
    {

        string name = typeof(T).Name;
        if (instance == null)
        {
            instance = GetComponent<T>();
            Debug.Log($"[{name}::Awake] BaseSingleton object initialized.");
        }
        else
        {
            if (gameObject.GetComponents<Component>().Length > 1)
            {
                Debug.Log($"[{name}::Awake] '{name}' already created! GameObject has other components, so just destroying newly created component.");
                DestroyImmediate(this);
            }
            else
            {
                Debug.Log($"[{name}::Awake] '{gameObject.name}' already created! Destroying newly created one");
                DestroyImmediate(gameObject);
            }
        }
    }

    private void OnApplicationQuit()
    {
        isDestroyed = true;
    }
}