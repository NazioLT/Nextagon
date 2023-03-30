using TMPro;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Nazio_LT.Tools.Core;
using UnityEngine.UI;

namespace Nazio_LT.Tools.UI
{
    [AddComponentMenu("Nazio_LT/UI/NDropdown")]
    public class NDropdown : TMP_Dropdown
    {
        [SerializeField] private TextMeshProUGUI label;
        [SerializeField] private NAudio selectSound;
        [SerializeField] private float ddRelativeSize;
        [SerializeField] private Image selectedImage;

        private System.Action<int> onValueChangeActionInit;

        public void Init(System.Action<int> _callBack, string _label)
        {
            onValueChanged.AddListener((_v) => _callBack(_v));
            SetLabel(_label);
        }

        public void Init(System.Action<int> _callBack, string _label, List<string> _labels, int _value)
        {
            Init(_callBack, _label);
            SetLabels(_labels, _value);
        }

        public void SetLabel(string _label)
        {
            if (label != null) label.text = $"{_label} :";
        }

        public void SetLabels(List<string> _labels, int _value)
        {
            ClearOptions();
            AddOptions(_labels);
            value = _value;
            RefreshShownValue();
        }

        public override void OnSubmit(BaseEventData eventData)
        {
            base.OnSubmit(eventData);

            if (selectSound) NAudioManager.instance.PlayAudio(selectSound);

            onValueChangeActionInit(value);
        }

        protected override void DoStateTransition(SelectionState _state, bool _instant)
        {
            base.DoStateTransition(_state, _instant);

            if (selectedImage) selectedImage.enabled = _state == SelectionState.Selected || _state == SelectionState.Highlighted;
        }

        #region Editor

        public virtual void UpdateElementsSize()
        {
            RectTransform _rect = (RectTransform)transform;

            _rect.SetAnchorsX(ddRelativeSize, 1f);
            label.rectTransform.SetAnchorsX(0, ddRelativeSize);
        }

        #endregion
    }
}