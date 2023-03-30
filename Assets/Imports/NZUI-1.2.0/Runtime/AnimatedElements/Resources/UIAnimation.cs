using System.Collections.Generic;
using UnityEngine;
using Nazio_LT.Tools.NTween;

namespace Nazio_LT.Tools.UI
{
    [System.Serializable]
    public class UIAnimation
    {
        [SerializeField] private float openWaitingTime = 0f, closeWaitingTime = 0f;
        [SerializeField] private UIAnimationSettings[] animationSettings;

        private List<NTweener> tweeners = new List<NTweener>();

        public void Anim(AnimatedUIElement _element, bool _opening)
        {
            tweeners = new List<NTweener>();

            if (animationSettings == null || animationSettings.Length == 0)
            {
                tweeners.Add(UIAnimationSettings.InstantAnim(_element, _opening).UnscaledTime().WaitBeforeStart(WaitingTime(_opening)).StartTween());
                return;
            }

            foreach (var _anim in animationSettings)
            {
                NTweener _tween = _anim.GetAnimationTweener(_element, _opening);
                _tween.UnscaledTime().WaitBeforeStart(WaitingTime(_opening)).StartTween();

                tweeners.Add(_tween);
            }
        }

        private float WaitingTime(bool _opening) => _opening ? openWaitingTime : closeWaitingTime;

        public void Stop()
        {
            if (tweeners.Count == 0) return;
            foreach (var _tweener in tweeners)
            {
                _tweener.Stop(false);
            }
        }
    }
}