using UnityEngine;
[CreateAssetMenu(fileName = "TransformerSO", menuName = "ScriptableObjects/TransformerSO")]
public class TransformerSO : ScriptableObject
{
    public float TransformDelay;
    public GameObject TransformedPrefab;
}