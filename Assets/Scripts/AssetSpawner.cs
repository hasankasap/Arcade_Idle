using System.Collections;
using UnityEngine;

public class AssetSpawner : MonoBehaviour
{
    [SerializeField] private SpawnerSO spawnerSO;
    [SerializeField] private Transform spawnAreaCenter;
    private PickUpArea pickupArea;

    private IEnumerator spawnCoroutine;

    private float spawntTimer => spawnerSO.SpawnTimer;
    private Product prefab => spawnerSO.SpawnPrefab;

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
        //GameObject temp = Instantiate(Prefab, pickupArea.GetStoragePoint(), Prefab.transform.rotation, pickupArea.transform);
        Product tempProduct = ProductPool.Instance.GetObjectFromPool(prefab, prefab.name);
        tempProduct.transform.parent = pickupArea.transform;
        tempProduct.transform.position = pickupArea.GetStoragePoint();
        tempProduct.transform.localScale = prefab.transform.localScale;
        tempProduct.gameObject.SetActive(true);
        tempProduct.transform.rotation = prefab.transform.rotation;
        pickupArea.AddProduct(tempProduct);
    }
    private IEnumerator SpawnWithTimer()
    {
        while (true)
        {
            yield return new WaitUntil(() => pickupArea.CanAdd());
            SpawnAsset();
            yield return new WaitForSeconds(spawntTimer);
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