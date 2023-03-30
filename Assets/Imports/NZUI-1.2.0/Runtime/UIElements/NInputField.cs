using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using Nazio_LT.Tools.Core;
using UnityEngine.UI;

namespace Nazio_LT.Tools.UI
{
    [AddComponentMenu("Nazio_LT/UI/NInputField")]
    public class NInputField : TMP_InputField
    {
        [SerializeField] private TextMeshProUGUI label;
        [SerializeField] private NAudio stateChangeSound;
        [SerializeField] private Image selectedImage;

        private System.Action<string> _onClickCallback;

        public void Init(System.Action<string> _callBack, string _label)
        {
            _onClickCallback = _callBack;
            SetLabel(_label);
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

            if (_onClickCallback != null) _onClickCallback(text);
        }
    }
}