#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace Nazio_LT.Tools.UI.Internal
{
    [CanEditMultipleObjects, CustomEditor(typeof(NButton))]
    public class NButtonEditor : UnityEditor.UI.ButtonEditor
    {
        private SerializedProperty label_Prop;
        private SerializedProperty labelColorChange_Prop;
        private SerializedProperty labelColor_Prop;

        private SerializedProperty onClickSFX_Prop;

        private NButton button;

        protected override void OnEnable()
        {
            base.OnEnable();

            button = (NButton)target;

            label_Prop = serializedObject.FindProperty("label");
            labelColorChange_Prop = serializedObject.FindProperty("labelColorChange");
            labelColor_Prop = serializedObject.FindProperty("labelColor");

            onClickSFX_Prop = serializedObject.FindProperty("onClickSFX");
        }

        public override void OnInspectorGUI()
        {
            DrawProps();

            serializedObject.ApplyModifiedProperties();
            serializedObject.Update();

            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Unity Button", EditorStyles.boldLabel);
            base.OnInspectorGUI();
        }

        protected virtual void DrawProps()
        {
            EditorGUILayout.LabelField("NButton", EditorStyles.boldLabel);

            EditorGUILayout.ObjectField(label_Prop, new GUIContent(label_Prop.displayName));
            if (button.HasLabel)
            {
                labelColorChange_Prop.boolValue = EditorGUILayout.Toggle(labelColorChange_Prop.displayName, labelColorChange_Prop.boolValue);
                if(labelColorChange_Prop.boolValue)
                {
                    EditorGUILayout.PropertyField(labelColor_Prop, new GUIContent(labelColor_Prop.displayName));
                }
            }

            EditorGUILayout.Space();

            EditorGUILayout.ObjectField(onClickSFX_Prop, new GUIContent(onClickSFX_Prop.displayName));
        }
    }
}
#endif