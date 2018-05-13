
namespace NaturalLocomotion
{
    using UnityEngine;

    public interface IModifierHandler
    {
        Vector3 ModifyDirection(Vector3 direction);
    }
}