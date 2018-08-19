namespace NaturalLocomotion
{
    using UnityEngine;

    public class AirControlModifier : ModifierHandler
    {
        [SerializeField] private float _airControlScale;

        public override float Calculate(float currentModifier)
        {
            return currentModifier * _airControlScale;
        }
    }
}