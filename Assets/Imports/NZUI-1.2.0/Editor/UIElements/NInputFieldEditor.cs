#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace Nazio_LT.Tools.UI.Internal
{
    [CanEditMultipleObjects, CustomEditor(typeof(NInputField))]
    public class NInputFieldEditor : TMPro.EditorUtilities.TMP_InputFieldEditor
    {
        private SerializedProperty label_Prop;
        private SerializedProperty stateChangeSound_Prop;
        private SerializedProperty selectedImage_Prop;

        protected override void OnEnable()
        {
            base.OnEnable();

            label_Prop = serializedObject.FindProperty("label");

            stateChangeSound_Prop = serializedObject.FindProperty("stateChangeSound");
            selectedImage_Prop = serializedObject.FindProperty("selectedImage");
        }

        public override void OnInspectorGUI()
        {
            NInputField _toggle = (NInputField)target;

            DrawProps();

            serializedObject.ApplyModifiedProperties();
            serializedObject.Update();

            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Unity Toggle", EditorStyles.boldLabel);
            base.OnInspectorGUI();
        }

        protected virtual void DrawProps()
        {
            EditorGUILayout.LabelField("NToggle", EditorStyles.boldLabel);

            EditorGUILayout.ObjectField(label_Prop, new GUIContent(label_Prop.displayName));

            EditorGUILayout.ObjectField(stateChangeSound_Prop, new GUIContent(stateChangeSound_Prop.displayName));
            EditorGUILayout.PropertyField(selectedImage_Prop);
        }
    }
}
#endif