using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

namespace Nazio_LT.Tools.UI
{
    [AddComponentMenu("Nazio_LT/UI/NSlider")]
    public class NSlider : Slider
    {
        [SerializeField, HideInInspector] private bool showBase;

        [SerializeField] private RectTransform main;
        [SerializeField] private TextMeshProUGUI labelText, percentText;
        [SerializeField] private bool showLabel, showPercent;
        [SerializeField, Range(0, 1)] private float sliderMin, sliderMax;
        [SerializeField] private Image selectedImage;

        private System.Action<float> onValueChangeActionInit;

        public void Init(string _label, System.Action<float> _callback, float _baseValue)
        {
            onValueChangeActionInit = _callback;
            onValueChanged.AddListener(OnChange);

            value = _baseValue;

            if (percentText) percentText.gameObject.SetActive(PercentShowed);

            if (PercentShowed) percentText.text = (value * 100f).ToString("0") + "%";
            SetLabel(_label);
        }

        public void SetLabel(string _label)
        {
            if (LabelShowed) labelText.text = $"{_label} :";
        }

        public void OnChange(float _value)
        {
            if (onValueChangeActionInit != null) onValueChangeActionInit(_value);

            if (PercentShowed) percentText.text = (_value * 100f).ToString("0") + "%";
        }

        protected override void DoStateTransition(SelectionState _state, bool _instant)
        {
            base.DoStateTransition(_state, _instant);

            if (selectedImage) selectedImage.enabled = _state == SelectionState.Selected || _state == SelectionState.Highlighted;
        }

        #region Editor

        public void UpdateElementSize()
        {
            sliderMin = Mathf.Clamp(sliderMin, 0f, sliderMax);
            sliderMax = Mathf.Clamp(sliderMax, sliderMin, 1f);

            if (percentText != null) percentText.gameObject.SetActive(showPercent);
            if (labelText != null) labelText.gameObject.SetActive(showLabel);

            if (main == null) return;

            float _min = 0;
            if (LabelShowed) _min = sliderMin;

            float _max = 1;
            if (PercentShowed) _max = sliderMax;

            main.SetAnchorsX(_min, _max);

            if (labelText != null) labelText.rectTransform.SetAnchorsX(0, _min);
            if (percentText != null) percentText.rectTransform.SetAnchorsX(_max, 1);
        }

        #endregion

        public bool PercentShowed => percentText != null && percentText.gameObject.activeSelf;
        public bool LabelShowed => labelText != null && labelText.gameObject.activeSelf;
    }
}