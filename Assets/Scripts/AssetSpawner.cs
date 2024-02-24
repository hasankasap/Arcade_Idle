using System.Collections;
using UnityEngine;

public class AssetSpawner : MonoBehaviour
{
    [SerializeField] private SpawnerSO spawnerSO;
    [SerializeField] private Transform spawnAreaCenter;
    private PickUpArea pickupArea;

    private IEnumerator spawnCoroutine;

    private float SpawntTimer => spawnerSO.SpawnTimer;
    private GameObject Prefab => spawnerSO.SpawnPrefab;

    void OnEnable()
    {
        EventManager.StartListening(GameEvents.GAME_STARTED, OnGameStarted);
    }
    void OnDisable()
    {
        EventManager.StopListening(GameEvents.GAME_STARTED, OnGameStarted);
    }

    private void Initialize()
    {
        pickupArea = GetComponentInChildren<PickUpArea>();
        if (spawnCoroutine != null)
            StopCoroutine(spawnCoroutine);
        spawnCoroutine = SpawnWithTimer();
        StartCoroutine(spawnCoroutine);
    }
    private void SpawnAsset()
    {
        GameObject temp = Instantiate(Prefab, pickupArea.GetStoragePoint(), Prefab.transform.rotation, pickupArea.transform);
        Product tempProduct = temp.GetComponent<Product>();
        pickupArea.AddProduct(tempProduct);
    }
    private IEnumerator SpawnWithTimer()
    {
        while (true)
        {
            yield return new WaitUntil(() => pickupArea.CanAdd());
            SpawnAsset();
            yield return new WaitForSeconds(SpawntTimer);
            yield return new WaitForFixedUpdate();
        }
    }
    public Transform GetSpawnAreaCenter()
    {
        return spawnAreaCenter;
    }
    private void OnGameStarted(object[] obj)
    {
        Initialize();
    }
}