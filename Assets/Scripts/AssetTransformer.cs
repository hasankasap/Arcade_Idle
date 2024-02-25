using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting;

public class AssetTransformer : MonoBehaviour
{
    [SerializeField] private TransformerSO transformerSO;
    [SerializeField] private Transform transformerInputPoint, transformerOutputPoint;
    [SerializeField] private Transform pickUpAreaCenter, dropAreaCenter;
    private DropArea dropArea;
    private PickUpArea pickUpAreaController;

    private IEnumerator transformCoroutine;

    private Product prefab => transformerSO.TransformedPrefab;
    private float transformDelay => transformerSO.TransformDelay;

    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        dropArea = GetComponentInChildren<DropArea>();
        pickUpAreaController = GetComponentInChildren<PickUpArea>();
        if (transformCoroutine != null)
            StopCoroutine(transformCoroutine);
        transformCoroutine = ChangeWithTimer();
        StartCoroutine(transformCoroutine);
    }
    private IEnumerator ChangeWithTimer()
    {
        while (true)
        {
            yield return new WaitUntil(() => dropArea.HasProduct() && pickUpAreaController.CheckCanAddInstantly());
            yield return new WaitForSeconds(transformDelay);
            ChangeProduct();
            yield return new WaitForFixedUpdate();
        }
    }
    private void ChangeProduct()
    {
        Vector3 spawnPos = pickUpAreaController.GetStoragePoint();
        Product tempProduct = dropArea.GetLastProduct();
        tempProduct.transform.DOScale(Vector3.zero, .6f).SetLink(tempProduct.gameObject).SetDelay(.1f).OnComplete(() => 
        {
            tempProduct.gameObject.SetActive(false);
            ProductPool.Instance.ReturnPool(tempProduct, tempProduct.gameObject.name);
        });
        tempProduct.transform.DOJump(transformerInputPoint.position, 2, 1, .5f).OnComplete(() =>
        {
            SpawnTransformedProduct(spawnPos);
            //Destroy(tempProduct);         
        });
    }
    private void SpawnTransformedProduct(Vector3 spawnPos)
    {
        //GameObject temp = Instantiate(prefab, transformerOutputPoint.position, prefab.transform.rotation, pickUpAreaController.transform);
        Product tempProduct = ProductPool.Instance.GetObjectFromPool(prefab, prefab.name);
        tempProduct.transform.parent = pickUpAreaController.transform;
        tempProduct.transform.position = transformerOutputPoint.position;
        tempProduct.transform.rotation = prefab.transform.rotation;
        tempProduct.gameObject.SetActive(true);
        tempProduct.transform.DOMove(spawnPos, .5f).OnComplete(() => pickUpAreaController.AddProduct(tempProduct));
    }
    public Transform GetDropAreaCenter()
    {
        return dropAreaCenter;
    }
    public Transform GetPicUpAreaCenter()
    {
        return pickUpAreaCenter;
    }
}