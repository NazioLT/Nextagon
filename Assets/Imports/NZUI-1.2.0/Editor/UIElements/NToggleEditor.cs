#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace Nazio_LT.Tools.UI.Internal
{
    [CanEditMultipleObjects, CustomEditor(typeof(NToggle))]
    public class NToggleEditor : UnityEditor.UI.ToggleEditor
    {
        private SerializedProperty label_Prop;
        private SerializedProperty stateChangeSound_Prop;

        protected override void OnEnable()
        {
            base.OnEnable();

            label_Prop = serializedObject.FindProperty("label");

            stateChangeSound_Prop = serializedObject.FindProperty("stateChangeSound");
        }

        public override void OnInspectorGUI()
        {
            NToggle _toggle = (NToggle)target;

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
        }
    }
}
#endif