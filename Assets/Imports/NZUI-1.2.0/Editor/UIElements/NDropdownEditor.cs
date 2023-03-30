#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace Nazio_LT.Tools.UI.Internal
{
    [CanEditMultipleObjects, CustomEditor(typeof(NDropdown))]
    public class NDropdownEditor : TMPro.EditorUtilities.DropdownEditor
    {
        private SerializedProperty label_Prop;
        private SerializedProperty selectSound_Prop;

        private SerializedProperty ddRelativeSize_Prop;

        private NDropdown ddTarget;

        protected override void OnEnable()
        {
            base.OnEnable();

            ddTarget = (NDropdown)target;

            label_Prop = serializedObject.FindProperty("label");

            selectSound_Prop = serializedObject.FindProperty("selectSound");
            ddRelativeSize_Prop = serializedObject.FindProperty("ddRelativeSize");
        }

        public override void OnInspectorGUI()
        {
            ddTarget.UpdateElementsSize();

            DrawProps();

            EditorGUILayout.Space();
            EditorGUILayout.LabelField("TMP_Dropdown", EditorStyles.boldLabel);

            base.OnInspectorGUI();

            serializedObject.ApplyModifiedProperties();
            serializedObject.Update();
        }

        protected virtual void DrawProps()
        {
            EditorGUILayout.LabelField("NDropdown", EditorStyles.boldLabel);

            EditorGUILayout.ObjectField(label_Prop, new GUIContent(label_Prop.displayName));
            ddRelativeSize_Prop.floatValue = EditorGUILayout.Slider("Dropdown Relative Size", ddRelativeSize_Prop.floatValue, 0f, 1f);
            
            EditorGUILayout.ObjectField(selectSound_Prop, new GUIContent(selectSound_Prop.displayName));

            serializedObject.ApplyModifiedProperties();
        }
    }
}
#endif