using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropArea : MonoBehaviour
{
    [SerializeField] private Storage productStorage;
    [SerializeField] private DropAreaSO dropAreaSO;
    private bool assetTeking = false;
    private float takeDelay => dropAreaSO.TakeDelay;
    private ProductTypes productType => productStorage.GetStorageType();

    private IEnumerator takeAssetCoroutine;
    private List<ICharacter> queCharacters = new List<ICharacter>();

    public Product GetLastProduct()
    {
        productStorage.Decrease();
        return productStorage.GetLastProduct();
    }
    public bool HasProduct()
    {
        return productStorage.HasProduct();
    }
    private IEnumerator TakeProductWithTimer()
    {
        while (assetTeking)
        {
            yield return new WaitUntil(() => !productStorage.IsStorageFull() && queCharacters.Count > 0);
            yield return new WaitForSeconds(takeDelay);
            TakeProduct();
            yield return new WaitForFixedUpdate();
        }
    }
    private void TakeProduct()
    {
        if (queCharacters.Count > 0 && queCharacters[0].CanDropWantedProductTypes(productType))
        {
            Product temp = queCharacters[0].DropProductsWithType(productType);
            Vector3 localAngle = temp.transform.localEulerAngles;
            temp.transform.parent = productStorage.StoragePoint.parent;
            productStorage.AddProduct(temp);
            temp.transform.DOLocalRotate(localAngle, .5f, RotateMode.FastBeyond360);
            Vector3 pos = productStorage.GetStoragePoint();
            temp.transform.DOLocalJump(pos, 2, 1, .5f);
            productStorage.Increase();
        }
        else if (queCharacters.Count > 0 && !queCharacters[0].CanDropWantedProductTypes(productType))
        {
            queCharacters.RemoveAt(0);
            if (queCharacters.Count == 0)
            {
                StartTakeCoroutine(false);
            }
        }
    }
    private void StartTakeCoroutine(bool startStatus)
    {
        assetTeking = startStatus;
        if (startStatus)
        {
            if (takeAssetCoroutine != null)
                StopCoroutine(takeAssetCoroutine);
            takeAssetCoroutine = TakeProductWithTimer();
            StartCoroutine(takeAssetCoroutine);
        }
        else
        {
            if (takeAssetCoroutine != null)
                StopCoroutine(takeAssetCoroutine);
        }
    }

    public void OnCharacterEnter(Collider characterCollider)
    {
        ICharacter character = characterCollider.GetComponentInChildren<ICharacter>();
        if (character != null)
        {
            if (queCharacters.Count == 0)
            {
                StartTakeCoroutine(true);
            }
            if (!queCharacters.Contains(character))
                queCharacters.Add(character);
        }
    }
    public void OnCharacterExit(Collider characterCollider)
    {
        ICharacter character = characterCollider.GetComponentInChildren<ICharacter>();
        if (queCharacters.Contains(character))
        {
            queCharacters.Remove(character);
            if (queCharacters.Count == 0)
            {
                StartTakeCoroutine(false);
            }
        }
    }
}