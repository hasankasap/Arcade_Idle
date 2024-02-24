using UnityEngine;
[CreateAssetMenu(fileName = "StoragePropSO", menuName = "ScriptableObjects/StoragePropSO")]
public class StoragePropSO : ScriptableObject
{
    public float StorageLineOffset, StorageColumnOffset;
    public int StorageLineCapacity;
    public int StorageCapacity;
    public ProductTypes ProductType;
}