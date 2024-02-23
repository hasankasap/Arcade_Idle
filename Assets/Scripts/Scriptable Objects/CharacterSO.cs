using UnityEngine;
[CreateAssetMenu(fileName = "CharacterSO", menuName = "ScriptableObjects/CharacterSO")]
public class CharacterSO : ScriptableObject
{
    public float speed, rotationSpeed, sensitivity, acceleration;
    public int stackCapacity;
}