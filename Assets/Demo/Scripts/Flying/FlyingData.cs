    using UnityEngine;

[CreateAssetMenu(fileName = "FlyingData", menuName = "Flying Data")]
public class FlyingData : ScriptableObject
{
    [SerializeField] private float _speedModifier;
    [SerializeField] private float _minimumSprintValue;

    public float speedModifier { get { return _speedModifier; } }
    public float minimumSprintValue { get { return _minimumSprintValue; } }
}