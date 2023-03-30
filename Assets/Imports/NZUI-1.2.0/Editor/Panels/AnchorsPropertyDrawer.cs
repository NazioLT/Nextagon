#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using Nazio_LT.Tools.Core.Internal;

namespace Nazio_LT.Tools.UI.Internal
{
    [CanEditMultipleObjects, CustomPropertyDrawer(typeof(Anchors))]
    public class AnchorsPropertyDrawer : NPropertyDrawer
    {
        private SerializedProperty min_Prop, max_Prop;

        protected override void DefineProps(SerializedProperty _property)
        {
            min_Prop = _property.FindPropertyRelative("min");
            max_Prop = _property.FindPropertyRelative("max");
        }

        protected override void DrawGUI(Rect _position, SerializedProperty _property, GUIContent _label, ref float _propertyHeight, ref Rect _baseRect)
        {
            NEditor.DrawHeader(_property.displayName, ref _baseRect, ref _propertyHeight);
            NEditor.DrawMultipleGUIClassic(_baseRect, _propertyHeight, new SerializedProperty[] { min_Prop, max_Prop });
        }
    }
}
#endif