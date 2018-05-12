namespace NaturalLocomotion
{
    using UnityEngine;

    public class NaturalInput : HubConnector
    {
        public virtual Vector3 GetVector()
        {
            return Vector3.zero;
        }
    }
}