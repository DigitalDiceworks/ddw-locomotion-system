namespace NaturalLocomotion
{
    using System;
    using UnityEngine;

    [Serializable]
    public class GroundHandlerBase : MonoBehaviour
    {
        public bool isOnGround { get; protected set; }
        public event Action onLand;
        public event Action onTakeoff;

        protected virtual void Start()
        {
            isOnGround = true;
        }

        protected void Land()
        {
            isOnGround = true;
            if (onLand != null)
            {
                onLand();
            }
        }

        protected void Takeoff()
        {
            isOnGround = false;
            if (onTakeoff != null)
            {
                onTakeoff();
            }
        }
    }
}