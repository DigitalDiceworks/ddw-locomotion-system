namespace NaturalLocomotion
{
    using UnityEngine;

    public class SprintModifier : ModifierHandler
    {
        [SerializeField] private float _sprintScale;

        public override float Calculate(float currentModifier)
        {
            return currentModifier * _sprintScale;
        }
    }
}