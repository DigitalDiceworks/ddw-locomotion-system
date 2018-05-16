namespace NaturalLocomotion
{
    using UnityEngine;

    /// <summary>
    /// Base class for input handlers that connect to Locomotion hub
    /// and provide input vectors to move the player
    /// </summary>
    public class NaturalInput : HubConnector
    {
        /// <summary>
        /// Gets the current input vector, will only be called if the input is active.
        /// </summary>
        /// <returns>Normalized input for the current frame</returns>
        public virtual Vector3 GetVector()
        {
            return Vector3.zero;
        }
    }
}