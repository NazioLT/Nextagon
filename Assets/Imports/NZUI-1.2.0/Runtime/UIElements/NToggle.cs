using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using Nazio_LT.Tools.Core;

namespace Nazio_LT.Tools.UI
{
    [AddComponentMenu("Nazio_LT/UI/NToggle")]
    public class NToggle : Toggle
    {
        [SerializeField] private TextMeshProUGUI label;
        [SerializeField] private NAudio stateChangeSound;
        [SerializeField] private Image selectedImage;

        private System.Action<bool> _onClickCallback;

        public void Init(System.Action<bool> _callBack, string _label)
        {
            _onClickCallback = _callBack;
            SetLabel(_label);
        }

        public void Init(System.Action<bool> _callBack, string _label, bool _baseValue)
        {
            Init(_callBack, _label);

            isOn = _baseValue;
        }

        public void SetLabel(string _label)
        {
            if (label != null) label.text = $"{_label} :";
        }

        protected override void DoStateTransition(SelectionState _state, bool _instant)
        {
            base.DoStateTransition(_state, _instant);
            if (selectedImage) selectedImage.enabled = _state == SelectionState.Selected || _state == SelectionState.Highlighted;
        }

        public override void OnSubmit(BaseEventData eventData)
        {
            base.OnSubmit(eventData);

            if (stateChangeSound != null) NAudioManager.instance.PlayAudio(stateChangeSound);

            _onClickCallback(isOn);
        }
    }
}