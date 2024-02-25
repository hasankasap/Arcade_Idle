using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashController : MonoBehaviour
{
    [SerializeField] private Transform trashInputPoint;
    [SerializeField] private TrashCanSO trashCanSO;
    [SerializeField] private Transform transhCanDepositPoint;
    private List<ICharacter> queCharacters = new List<ICharacter>();
    private IEnumerator takeAssetCoroutine;
    private bool assetTaking = false;
    private float TakeDelay => trashCanSO.TakeDelay;

    private IEnumerator TakeProductWithTimer()
    {
        while (assetTaking)
        {
            yield return new WaitUntil(() => queCharacters.Count > 0);
            yield return new WaitForSeconds(TakeDelay);
            TakeProduct();
            yield return new WaitForFixedUpdate();
        }
    }
    private void TakeProduct()
    {
        if (queCharacters.Count > 0 && queCharacters[0].CanDropProductToTrash())
        {
            Product temp = queCharacters[0].DropToTrash();
            Vector3 localAngle = temp.transform.localEulerAngles;
            temp.transform.parent = transform;
            temp.transform.DOLocalJump(trashInputPoint.localPosition, 2, 1, .5f).OnComplete(() => 
            {
                //Destroy(temp.gameObject);
                temp.gameObject.SetActive(false);
                ProductPool.Instance.ReturnPool(temp, temp.gameObject.name);
            });
        }
        else if (queCharacters.Count > 0 && !queCharacters[0].CanDropProductToTrash())
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
        assetTaking = startStatus;
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
    public Transform GetTranshCanDepositPoint()
    {
        return transhCanDepositPoint;
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