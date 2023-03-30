#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace Nazio_LT.Tools.UI.Internal
{
    [CanEditMultipleObjects, CustomEditor(typeof(NSlider))]
    public class NSliderEditor : UnityEditor.UI.SliderEditor
    {
        private SerializedProperty showBase_Prop;

        private SerializedProperty labelText_Prop, percentText_Prop, main_Prop;
        private SerializedProperty showLabel_Prop, showPercent_Prop;
        private SerializedProperty sliderMin_Prop, sliderMax_Prop;

        protected override void OnEnable()
        {
            base.OnEnable();

            showBase_Prop = serializedObject.FindProperty("showBase");

            labelText_Prop = serializedObject.FindProperty("labelText");
            percentText_Prop = serializedObject.FindProperty("percentText");
            main_Prop = serializedObject.FindProperty("main");

            sliderMin_Prop = serializedObject.FindProperty("sliderMin");
            sliderMax_Prop = serializedObject.FindProperty("sliderMax");

            showLabel_Prop = serializedObject.FindProperty("showLabel");
            showPercent_Prop = serializedObject.FindProperty("showPercent");
        }

        public override void OnInspectorGUI()
        {
            NSlider _slider = (NSlider)target;

            DrawProps();

            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Unity Slider", EditorStyles.boldLabel);
            bool _base = EditorGUILayout.Toggle("Show", showBase_Prop.boolValue);

            if (_base) base.OnInspectorGUI();

            showBase_Prop.boolValue = _base;

            serializedObject.ApplyModifiedProperties();
            serializedObject.Update();

            _slider.UpdateElementSize();
        }

        protected virtual void DrawProps()
        {
            EditorGUILayout.LabelField("NSlider", EditorStyles.boldLabel);

            EditorGUILayout.ObjectField(main_Prop, new GUIContent(main_Prop.displayName));

            EditorGUILayout.Space();

            showLabel_Prop.boolValue = EditorGUILayout.Toggle(showLabel_Prop.displayName, showLabel_Prop.boolValue);
            if (showLabel_Prop.boolValue)
            {
                EditorGUILayout.ObjectField(percentText_Prop, new GUIContent(percentText_Prop.displayName));
                sliderMin_Prop.floatValue = EditorGUILayout.Slider(sliderMin_Prop.displayName, sliderMin_Prop.floatValue, 0f, 1f);
            }

            EditorGUILayout.Space();

            showPercent_Prop.boolValue = EditorGUILayout.Toggle(showPercent_Prop.displayName, showPercent_Prop.boolValue);
            if (showPercent_Prop.boolValue)
            {
                EditorGUILayout.ObjectField(labelText_Prop, new GUIContent(labelText_Prop.displayName));
                sliderMax_Prop.floatValue = EditorGUILayout.Slider(sliderMax_Prop.displayName, sliderMax_Prop.floatValue, 0f, 1f);
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}
#endif