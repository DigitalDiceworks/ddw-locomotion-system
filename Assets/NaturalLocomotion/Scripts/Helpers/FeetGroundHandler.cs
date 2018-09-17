namespace NaturalLocomotion
{
    using UnityEngine;

    public class FeetGroundHandler : GroundHandlerBase
    {
        [SerializeField] private LayerMask _collisionMask;

        private void OnCollisionEnter(Collision collision)
        {
            Debug.Log("Collision with " + collision.gameObject.name);
            if (IsInLayer(_collisionMask, collision.gameObject.layer))
            {
                Debug.Log("Land");
                Land();
            }
        }

        private void OnCollisionExit(Collision collision)
        {
            Debug.Log("Exit Collision with " + collision.gameObject.name);
            if (IsInLayer(_collisionMask, collision.gameObject.layer))
            {
                Debug.Log("Take off");
                Takeoff();
            }
        }

        private bool IsInLayer(LayerMask mask, int layer)
        {
            return mask == (mask | (1 << layer));
        }
    }
}