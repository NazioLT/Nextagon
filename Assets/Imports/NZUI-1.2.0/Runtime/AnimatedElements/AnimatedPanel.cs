using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Nazio_LT.Tools.UI
{
    [AddComponentMenu("Nazio_LT/UI/Animated Panel")]
    [RequireComponent(typeof(Canvas), typeof(CanvasGroup))]
    public class AnimatedPanel : AnimatedUIElement
    {
        [SerializeField] private GameObject firstSelectedElement;
        [Space]
        [SerializeField] private UnityEvent rightShoulderEvent, leftShoulderEvent;

        private CanvasGroup group;
        private Canvas canvas;
        private EventSystem eventSystem;

        public override void Init()
        {
            base.Init();

            group = GetComponent<CanvasGroup>();
            canvas = GetComponent<Canvas>();
            eventSystem = FindObjectOfType<EventSystem>();
        }

        /// <summary>Show the panel and select it.</summary>
        public void SelectPanel()
        {
            eventSystem.SetSelectedGameObject(firstSelectedElement);

            Show();
        }

        #region Inputs

        public void LeftShoulder(InputAction.CallbackContext _ctx)
        {
            if (!_ctx.performed) return;
            leftShoulderEvent.Invoke();
        }

        public void RightShoulder(InputAction.CallbackContext _ctx)
        {
            if (!_ctx.performed) return;
            rightShoulderEvent.Invoke();
        }

        #endregion

        #region Animations

        public override void FadeAnim(float _t)
        {
            canvas.enabled = true;
            
            if (_t <= 0f || _t >= 1f)
            {
                canvas.enabled = _t > 0;
                group.alpha = _t <= 0 ? 0f : 1f;
                return;
            }

            group.alpha = _t;
        }

        #endregion
    }
}