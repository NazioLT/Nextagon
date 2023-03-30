using UnityEngine;
using Nazio_LT.Tools.NTween;

namespace Nazio_LT.Tools.UI
{
    public enum TransitionType { Fade, Translation, Scale }

    [System.Serializable]
    public class UIAnimationSettings
    {
        [SerializeField] private TransitionType type;
        [SerializeField] private float duration;
        [SerializeField] private AnimationCurve animationCurve = AnimationCurve.Linear(0, 0, 1, 1);

        [SerializeField] private Anchors showedAnchors, hiddenAnchors;
        [SerializeField] private Vector3 showedScale, hiddenScale;

        [SerializeField] private float propHeight = 60f;

        public NTweener GetAnimationTweener(AnimatedUIElement _element, bool _opening)
        {
            System.Func<float, float> _tConversionMethod = _opening ? (_t) => _t : ((_t) => 1 - _t);
            System.Action<float> _action = GetAnimationAction(_element);

            return NTweening.NTBuild((_t) => _action(_tConversionMethod(_t)), duration).AddTimeCurve(animationCurve);
        }

        private System.Action<float> GetAnimationAction(AnimatedUIElement _element)
        {
            switch (type)
            {
                case TransitionType.Fade:
                    return _element.FadeAnim;

                case TransitionType.Translation:
                    return (_t) => _element.AnchoredTranslation(_t, hiddenAnchors, showedAnchors);

                case TransitionType.Scale:
                    return (_t) => _element.Scale(_t, hiddenScale, showedScale);
            }

            return _element.Instant;
        }

        public static float GetPropHeight(TransitionType _type)
        {
            switch (_type)
            {
                default: return 60f;

                case TransitionType.Translation: return 180f;
                case TransitionType.Scale: return 100f;
            }
        }

        public static NTweener InstantAnim(AnimatedUIElement _element, bool _opening) => NTweening.NTBuild((_t) => _element.Instant(_opening ? 1 : 0), 0);
    }
}