using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

public class StackController : MonoBehaviour
{
    private List<Product> stackedProducts = new List<Product>();
    private int capacity = 5;
    [SerializeField] private Transform stackPoint;
    [SerializeField] private float stackUpOffset;
    private Vector3 lastLocalPos;

    public void Initialize(int capacity)
    {
        this.capacity = capacity;
    }
    public void AddStack(Product product)
    {
        if (stackPoint == null)
            stackPoint = transform;
        Vector3 localAngles = product.transform.localEulerAngles;
        product.transform.parent = stackPoint;
        Vector3 stackPos = Vector3.zero;
        if (stackedProducts.Count > 0)
        {
            if (product.type == ProductTypes.Product)
                stackPos.y = lastLocalPos.y + stackUpOffset;
            else
                stackPos.y = lastLocalPos.y + (stackUpOffset / 2f);
        }
        lastLocalPos = stackPos;
        product.transform.DOLocalRotate(localAngles, .5f, RotateMode.FastBeyond360);
        product.transform.DOLocalJump(stackPos, 2f, 1, .5f);
        stackedProducts.Add(product);
    }
    public bool IsStackFull()
    {
        return stackedProducts.Count >= capacity;
    }
    public bool IsStackHasWantedProducts(ProductTypes type)
    {
        return stackedProducts.Exists(x => x.type == type);
    }
    public Product GetLastProductWithType(ProductTypes type)
    {
        if (stackedProducts.Count == 0) return null;
        Product asset = stackedProducts.FindLast(x => x.type == type);
        stackedProducts.Remove(asset);
        if (stackedProducts.Count > 0)
            lastLocalPos = stackedProducts[stackedProducts.Count - 1].transform.position;
        else lastLocalPos = Vector3.zero;
        RePositionStackedProducts();
        return asset;
    }
    private void RePositionStackedProducts()
    {
        for (int i = 0; i < stackedProducts.Count; i++)
        {
            Vector3 stackPos = Vector3.zero;
            stackPos.y += i * stackUpOffset;
            stackedProducts[i].transform.localPosition = stackPos;
        }
    }
    public Product GetLastProduct()
    {
        if (stackedProducts.Count == 0) return null;
        Product product = stackedProducts[stackedProducts.Count - 1];
        stackedProducts.Remove(product);
        return product;
    }
    public bool IsStackHasAnyProduct()
    {
        return stackedProducts.Count > 0;
    }

}