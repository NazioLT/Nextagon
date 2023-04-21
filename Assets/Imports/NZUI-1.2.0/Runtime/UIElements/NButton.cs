using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Nazio_LT.Tools.Core;

namespace Nazio_LT.Tools.UI
{
    [AddComponentMenu("Nazio_LT/UI/NButton")]
    public class NButton : Button
    {
        //Label Options
        [SerializeField] private TextMeshProUGUI label;
        [SerializeField] private bool labelColorChange;
        [SerializeField] private ColorBlock labelColor = ColorBlock.defaultColorBlock;

        //Audio
        [SerializeField] private NAudio onClickSFX;

        private System.Action onClickActionInit;

        public void Init(System.Action _onClickCallBack, string _label)
        {
            onClickActionInit = _onClickCallBack;
            SetLabel(_label);
        }

        public void SetLabel(string _label)
        {
            if (label != null) label.text = $"{_label} :";
        }

        public override void OnPointerClick(PointerEventData eventData)
        {
            base.OnPointerClick(eventData);

            if(onClickSFX != null) NAudioManager.instance.PlayAudio(onClickSFX);

            if(onClickActionInit != null) onClickActionInit();
        }

        protected override void DoStateTransition(SelectionState state, bool instant)
        {
            base.DoStateTransition(state, instant);

            if (!labelColorChange) return;

            switch (state)
            {
                case SelectionState.Normal:
                    label.color = labelColor.normalColor;
                    break;
                case SelectionState.Highlighted:
                    label.color = labelColor.highlightedColor;
                    break;
                case SelectionState.Disabled:
                    label.color = labelColor.disabledColor;
                    break;
                case SelectionState.Pressed:
                    label.color = labelColor.pressedColor;
                    break;
                case SelectionState.Selected:
                    label.color = labelColor.selectedColor;
                    break;
            }


        }

        public bool HasLabel => label != null;
        public string LabelText => HasLabel ? label.text : "";
    }
}