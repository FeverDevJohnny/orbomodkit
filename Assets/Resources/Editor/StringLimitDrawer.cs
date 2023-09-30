using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(LimitString))]
public class StringLimitDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginChangeCheck();
        int maxLength = (attribute as LimitString).maxLength;

        property.stringValue = EditorGUI.TextField(position, label, property.stringValue);

        if (EditorGUI.EndChangeCheck())
        {
            if (property.stringValue.Length > maxLength)
            {
                property.stringValue = property.stringValue.Substring(0, maxLength);

                GUI.FocusControl(null);
            }
        }
    }
}

[CustomPropertyDrawer(typeof(LimitStringTextArea))]
public class StringLimitTextAreaDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginChangeCheck();
        int maxLength = (attribute as LimitStringTextArea).maxLength;
        float height = (attribute as LimitStringTextArea).height;


        position.height = 8f * height;
        EditorGUILayout.LabelField(property.displayName);
        property.stringValue = EditorGUILayout.TextArea(property.stringValue, GUI.skin.textArea, GUILayout.Height(height * 8)); //EditorGUI.TextField(position, label, property.stringValue);

        if (EditorGUI.EndChangeCheck())
        {
            if (property.stringValue.Length > maxLength)
            {
                property.stringValue = property.stringValue.Substring(0, maxLength);

                GUI.FocusControl(null);
            }
        }
    }
}
