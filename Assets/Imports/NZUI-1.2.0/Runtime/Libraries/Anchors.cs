using UnityEngine;

namespace Nazio_LT.Tools.UI
{
    [System.Serializable]
    public struct Anchors
    {
        public Anchors(Vector2 _min, Vector2 _max)
        {
            min = _min;
            max = _max;
        }

        [SerializeField] private Vector2 min, max;

        public static Anchors Lerp(Anchors _a, Anchors _b, float _t)
        {
            return new Anchors(
                Vector2.Lerp(_a.min, _b.min, _t),
                Vector2.Lerp(_a.max, _b.max, _t)
            );
        }

        public Vector2 Min => min;
        public Vector2 Max => max;
    }
}