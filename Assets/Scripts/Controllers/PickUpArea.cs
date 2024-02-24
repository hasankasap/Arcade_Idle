using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpArea : MonoBehaviour
{
    private List<ICharacter> queCharacters = new List<ICharacter>();

    [SerializeField] private Storage storageProperties;
    [SerializeField] private PickUpAreaSO pickUpAreaSO;

    private bool assetsSending = false;
    private int currentCount = 0;

    private IEnumerator sendAssetCoroutine;
    private float Delay => pickUpAreaSO.SendDelay;

    public Vector3 GetStoragePoint()
    {
        Vector3 pos = storageProperties.GetStoragePoint();
        pos = storageProperties.StoragePoint.parent.TransformPoint(pos);
        storageProperties.Increase();
        currentCount++;
        return pos;
    }
    public void AddProduct(Product product)
    {
        storageProperties.AddProduct(product);
    }
    public bool CheckCanAddInstantly()
    {
        return !(currentCount >= storageProperties.GetCapacity());
    }
    public bool CanAdd()
    {
        return !storageProperties.IsStorageFull();
    }
    private void SendProduct()
    {
        if (queCharacters.Count > 0 && queCharacters[0].CanTakeProducts())
        {
            Product asset = storageProperties.GetLastProduct();
            storageProperties.Decrease();
            queCharacters[0].TakeProducts(asset);
            currentCount--;
        }
        else if (queCharacters.Count > 0 && !queCharacters[0].CanTakeProducts())
        {
            queCharacters.RemoveAt(0);
            if (queCharacters.Count == 0)
            {
                StartSendingCoroutine(false);
            }
        }
    }
    private IEnumerator SendWithDelay()
    {
        while (assetsSending)
        {
            yield return new WaitUntil(() => storageProperties.HasProduct() && queCharacters.Count > 0);
            yield return new WaitForSeconds(Delay);
            SendProduct();
            yield return new WaitForFixedUpdate();
        }
    }
    private void StartSendingCoroutine(bool startStatus)
    {
        assetsSending = startStatus;
        if (startStatus)
        {
            if (sendAssetCoroutine != null)
                StopCoroutine(sendAssetCoroutine);
            sendAssetCoroutine = SendWithDelay();
            StartCoroutine(sendAssetCoroutine);
        }
        else
        {
            if (sendAssetCoroutine != null)
                StopCoroutine(sendAssetCoroutine);
        }
    }
    public void OnCharacterEnter(Collider characterCollider)
    {
        ICharacter character = characterCollider.GetComponentInChildren<ICharacter>();
        if (character != null)
        {
            if (queCharacters.Count == 0)
            {
                StartSendingCoroutine(true);
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
                StartSendingCoroutine(false);
            }
        }
    }
}