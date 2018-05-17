namespace NaturalLocomotion
{
    using UnityEngine;

    /// <summary>
    /// Simple base class that can be used to automatically connect
    /// child components to a LocomotionHub
    /// </summary>
    public class HubConnector : MonoBehaviour
    {
        protected LocomotionHub hub { get; private set; }

        protected virtual void Awake()
        {
            hub = GetComponentInParent<LocomotionHub>();
        }
    }
}