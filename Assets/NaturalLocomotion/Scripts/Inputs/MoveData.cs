namespace NaturalLocomotion
{
    using UnityEngine;

    [CreateAssetMenu(fileName = "MoveData", menuName = "NaturalLocomotion/MoveData")]
    public class MoveData : ScriptableObject
    {
        [SerializeField] private float _speedModifier;
        [SerializeField] private float _minimumSprintValue;

        public float speedModifier {  get { return _speedModifier; } }
        public float minimumSprintValue {  get { return _minimumSprintValue; } }
    }
}