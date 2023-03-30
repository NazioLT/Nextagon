#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using Nazio_LT.Tools.Core.Internal;

namespace Nazio_LT.Tools.UI.Internal
{
    [CanEditMultipleObjects, CustomPropertyDrawer(typeof(UIAnimationSettings))]
    public class UIAnimationSettingsPropertyDrawer : NPropertyDrawer
    {
        //Obligatoires
        private SerializedProperty type_Prop, duration_Prop, animationCurve_Prop;

        private SerializedProperty showedAnchors_Prop, hiddenAnchors_Prop;
        private SerializedProperty showedScale_Prop, hiddenScale_Prop;

        protected override void DefineProps(SerializedProperty _property)
        {
            type_Prop = _property.FindPropertyRelative("type");
            duration_Prop = _property.FindPropertyRelative("duration");
            animationCurve_Prop = _property.FindPropertyRelative("animationCurve");

            showedAnchors_Prop = _property.FindPropertyRelative("showedAnchors");
            hiddenAnchors_Prop = _property.FindPropertyRelative("hiddenAnchors");

            showedScale_Prop = _property.FindPropertyRelative("showedScale");
            hiddenScale_Prop = _property.FindPropertyRelative("hiddenScale");
        }

        protected override void DrawGUI(Rect _position, SerializedProperty _property, GUIContent _label, ref float _propertyHeight, ref Rect _baseRect)
        {
            NEditor.DrawMultipleGUIClassic(_baseRect, 20f, new SerializedProperty[] { type_Prop, duration_Prop, animationCurve_Prop });
            NEditor.AdaptGUI(ref _baseRect, ref _propertyHeight, NEditor.SINGLE_LINE * 3);

            TransitionType _type = (TransitionType)type_Prop.intValue;
            _property.FindPropertyRelative("propHeight").floatValue = UIAnimationSettings.GetPropHeight(_type);



            if (_type == TransitionType.Fade) return;

            if (_type == TransitionType.Translation)
            {
                NEditor.DrawMultipleGUIClassic(_baseRect, NEditor.SINGLE_LINE * 3, new SerializedProperty[] { showedAnchors_Prop, hiddenAnchors_Prop });
                NEditor.AdaptGUI(ref _baseRect, ref _propertyHeight, NEditor.SINGLE_LINE * 3 * 2);
                return;
            }

            if (_type == TransitionType.Scale)
            {
                NEditor.DrawMultipleGUIClassic(_baseRect, NEditor.SINGLE_LINE, new SerializedProperty[] { showedScale_Prop, hiddenScale_Prop });
                NEditor.AdaptGUI(ref _baseRect, ref _propertyHeight, NEditor.SINGLE_LINE * 2);
                return;
            }
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label) => property.FindPropertyRelative("propHeight").floatValue;
    }
}
#endif