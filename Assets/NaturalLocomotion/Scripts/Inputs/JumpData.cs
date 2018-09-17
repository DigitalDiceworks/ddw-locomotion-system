namespace NaturalLocomotion
{
    using UnityEngine;

    [CreateAssetMenu(fileName = "JumpData", menuName = "NaturalLocomotion/Jump Data")]
    public class JumpData : ScriptableObject
    {
        [SerializeField] private float _jumpDistance;
        [SerializeField] private float _jumpTimeout;
        [SerializeField] private float _jumpScale;

        public float jumpDistance { get { return _jumpDistance; } }
        public float jumpTimeout { get { return _jumpDistance; } }
        public float jumpScale { get { return _jumpScale; } }
    }
}