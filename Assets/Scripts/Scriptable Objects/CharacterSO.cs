using UnityEngine;
[CreateAssetMenu(fileName = "CharacterSO", menuName = "ScriptableObjects/CharacterSO")]
public class CharacterSO : ScriptableObject
{
    public float Speed, RotationSpeed, Sensitivity, Acceleration;
    public int StackCapacity;
}