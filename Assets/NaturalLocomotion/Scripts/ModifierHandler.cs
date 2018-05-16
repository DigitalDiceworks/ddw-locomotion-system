
namespace NaturalLocomotion
{
    using UnityEngine;

    /// <summary>
    /// Modifiers allow you to modify the normalized input from inputs
    /// before they are passed into the movement.
    /// </summary>
    public interface IModifierHandler
    {
        /// <summary>
        /// Given an input vector ( or previously modified vector ) and 
        /// returns a modified vector
        /// </summary>
        /// <param name="direction">Current input or modified input direction</param>
        /// <returns>A new modified direction, does not have to be normalized</returns>
        Vector3 ModifyDirection(Vector3 direction);
    }
}