using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Storage
{
    public StoragePropSO StorageProp;
    public Transform StoragePoint;
    public List<Product> StoredProducts = new List<Product>();
    [HideInInspector] public int ColumnCount, LineCount;

    public void Increase()
    {
        ColumnCount++;
        if (ColumnCount >= StorageProp.StorageLineCapacity)
        {
            ColumnCount = 0;
            LineCount++;
        }
    }

    public void Decrease()
    {
        ColumnCount--;
        if (ColumnCount < 0)
        {
            LineCount--;
            ColumnCount = StorageProp.StorageLineCapacity - 1;
        }
    }

    public Product GetLastProduct()
    {
        Product temp = StoredProducts[StoredProducts.Count - 1];
        StoredProducts.Remove(temp);
        return temp;
    }

    public int GetCapacity()
    {
        return StorageProp.StorageCapacity;
    }

    public void AddProduct(Product product)
    {
        StoredProducts.Add(product);
    }

    public ProductTypes GetStorageType()
    {
        return StorageProp.ProductType;
    }

    public bool IsStorageFull()
    {
        return StoredProducts.Count >= StorageProp.StorageCapacity;
    }

    public bool HasProduct()
    {
        return StoredProducts.Count > 0;
    }

    public int GetStoredProductCount()
    {
        return StoredProducts.Count;
    }

    public Vector3 GetStoragePoint()
    {
        Vector3 point = StoragePoint.localPosition;
        Vector3 forwardOffset = Vector3.forward * StorageProp.StorageLineOffset * ColumnCount;
        Vector3 upwardOffset = Vector3.left * StorageProp.StorageColumnOffset * LineCount;
        point += (forwardOffset + upwardOffset);
        return point;
    }
}
