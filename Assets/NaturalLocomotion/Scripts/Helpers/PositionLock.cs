namespace NaturalLocomotion
{
    using UnityEngine;

    public class PositionLock : MonoBehaviour
    {
        [SerializeField] private Transform _other;

        private void Update()
        {
            Vector3 position = _other.position;
            position.y = transform.position.y;
            transform.position = position;
        }
    }
}