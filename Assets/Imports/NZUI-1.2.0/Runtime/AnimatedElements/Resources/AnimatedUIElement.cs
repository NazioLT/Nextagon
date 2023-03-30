using System.Collections;
using UnityEngine;

namespace Nazio_LT.Tools.UI
{
    public abstract class AnimatedUIElement : MonoBehaviour
    {
        [SerializeField] private UIAnimation animations;

        public RectTransform rectTransform { private set; get; }

        protected void Awake() => Init();

        public virtual void Init()
        {
            rectTransform = transform as RectTransform;
        }

        public void Hide()
        {
            animations.Anim(this, false);
        }

        /// <summary>Only Show the panel.</summary>
        public void Show()
        {
            animations.Anim(this, true);
        }

        public abstract void FadeAnim(float _t);
        public virtual void Instant(float _t) => FadeAnim(_t);

        public void AnchoredTranslation(float _t, Anchors _hidden, Anchors _show)
        {
            rectTransform.SetAnchors(Anchors.Lerp(_hidden, _show, _t));
        }
        
        public void Scale(float _t, Vector3 _hidden, Vector3 _show)
        {
            transform.localScale = Vector3.LerpUnclamped(_hidden, _show, _t);
        }
    }
}