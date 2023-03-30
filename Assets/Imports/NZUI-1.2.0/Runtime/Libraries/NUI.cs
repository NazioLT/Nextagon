using UnityEngine;

namespace Nazio_LT.Tools.UI
{
    public static class NUI
    {
        public static void SetAnchorsX(this RectTransform _rect, float _min, float _max)
        {
            _rect.anchorMin = new Vector2(_min, _rect.anchorMin.y);
            _rect.anchorMax = new Vector2(_max, _rect.anchorMax.y);
        }

        public static void SetAnchorsY(this RectTransform _rect, float _min, float _max)
        {
            _rect.anchorMin = new Vector2(_rect.anchorMin.x, _min);
            _rect.anchorMax = new Vector2(_rect.anchorMax.x, _max);
        }

        public static void SetAnchors(this RectTransform _rect, Anchors _anchors)
        {
            _rect.SetAnchorsX(_anchors.Min.x, _anchors.Max.x);
            _rect.SetAnchorsY(_anchors.Min.y, _anchors.Max.y);
        }

        public static void SetAnchors(this RectTransform _rect, Vector2 _anchors)
        {
            _rect.SetAnchorsX(_anchors.x, _anchors.x);
            _rect.SetAnchorsY(_anchors.y, _anchors.y);
        }
    }
}