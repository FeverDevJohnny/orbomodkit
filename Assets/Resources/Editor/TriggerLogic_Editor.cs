using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

[CustomPropertyDrawer(typeof(TriggerLogic))]
public class TriggerLogic_Editor : PropertyDrawer
{
    float paddingTop = 16f, paddingBottom = 16f;

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        GUIStyle styleB = GUI.skin.label;
        styleB.fontStyle = FontStyle.Bold;
        styleB.alignment = TextAnchor.MiddleCenter;

        EditorGUI.DrawRect(new Rect(position.x, position.y + paddingTop, position.width, GetPropertyHeight(property, label) - paddingBottom * 2f), new Color(0.2f, 0.2f, 0.2f, 1f));
        EditorGUI.DrawRect(new Rect(position.x, position.y + paddingTop, position.width, 20f), new Color(0.3f, 0.3f, 0.3f, 1f));
        GUI.Label(new Rect(position.x, position.y + paddingTop, position.width, 20f), new GUIContent("Logic Table - " +property.displayName), styleB);

        if (property.FindPropertyRelative("foldouts").arraySize != property.FindPropertyRelative("comparison").arraySize)
            property.FindPropertyRelative("foldouts").arraySize = property.FindPropertyRelative("comparison").arraySize;

        if (property.FindPropertyRelative("logic").arraySize != property.FindPropertyRelative("comparison").arraySize - 1 && property.FindPropertyRelative("comparison").arraySize > 0)
            property.FindPropertyRelative("logic").arraySize = property.FindPropertyRelative("comparison").arraySize - 1;

        float m_foldOutSpacer = 0f;
        for (int i = 0; i < property.FindPropertyRelative("comparison").arraySize; i++)
        {

            var compAddress = property.FindPropertyRelative("comparison").GetArrayElementAtIndex(i).FindPropertyRelative("triggerAddress");
            var operation = property.FindPropertyRelative("comparison").GetArrayElementAtIndex(i).FindPropertyRelative("operation");
            var refValue = property.FindPropertyRelative("comparison").GetArrayElementAtIndex(i).FindPropertyRelative("referenceValue");
            var foldOut = property.FindPropertyRelative("foldouts").GetArrayElementAtIndex(i);
            EditorGUI.indentLevel = 1;
            foldOut.boolValue = EditorGUI.Foldout(new Rect(position.x, position.y + m_foldOutSpacer + paddingTop + 22f, position.width * 0.8f, 20), foldOut.boolValue, (string.IsNullOrEmpty(property.FindPropertyRelative("comparison").GetArrayElementAtIndex(i).FindPropertyRelative("triggerAddress").stringValue)) ? "(Assign Me!)" : "(" + compAddress.stringValue + " " + ConvertCompToText((ComparisonOperation)operation.enumValueIndex) + " " + refValue.intValue.ToString() + ")", true);

            if (foldOut.boolValue)
            {
                EditorGUI.indentLevel = 2;

                compAddress.stringValue = EditorGUI.TextField(new Rect(position.x, position.y + m_foldOutSpacer + 22f + paddingTop + 22f, position.width * 0.8f, 20), "Trigger Address", compAddress.stringValue); ;
                operation.enumValueIndex = (int)((ComparisonOperation)EditorGUI.EnumPopup(new Rect(position.x, position.y + m_foldOutSpacer + 44f + paddingTop + 22f, position.width * 0.8f, 20), "Comparison Type", (ComparisonOperation)operation.enumValueIndex));
                refValue.intValue = EditorGUI.IntField(new Rect(position.x, position.y + m_foldOutSpacer + 66f + paddingTop + 22f, position.width * 0.8f, 20), "Reference Value", refValue.intValue);
            }

            EditorGUI.indentLevel = 0;

            m_foldOutSpacer += (foldOut.boolValue ? 88f : 22f) + 22f;

            if (i < property.FindPropertyRelative("comparison").arraySize - 1)
            {
                var logOp = property.FindPropertyRelative("logic").GetArrayElementAtIndex(i);
                logOp.enumValueIndex = (int)((LogicOperation)EditorGUI.EnumPopup(new Rect(position.x, position.y + m_foldOutSpacer - 20f + paddingTop + 22f, position.width * 0.2f, 20), (LogicOperation)logOp.enumValueIndex));
            }
        }

        if (GUI.Button(new Rect(position.x, position.y + Mathf.Max(m_foldOutSpacer - 22f, 0f) + paddingTop + 22f, 40f, 20), "+"))
            property.FindPropertyRelative("comparison").InsertArrayElementAtIndex(property.FindPropertyRelative("comparison").arraySize);

        if(property.FindPropertyRelative("comparison").arraySize != 0)
            if (GUI.Button(new Rect(position.x + 50f, position.y + m_foldOutSpacer - 22f + paddingTop + 22f, 40f, 20), "-"))
                property.FindPropertyRelative("comparison").DeleteArrayElementAtIndex(property.FindPropertyRelative("comparison").arraySize - 1);

        m_foldOutSpacer += 22f;
    }

    public string ConvertCompToText(ComparisonOperation x)
    {
        switch (x)
        {
            case ComparisonOperation.Equals:
                return "==";

            case ComparisonOperation.NotEquals:
                return "!=";

            case ComparisonOperation.Less:
                return "<";

            case ComparisonOperation.LessEqual:
                return "<=";

            case ComparisonOperation.Greater:
                return ">";

            case ComparisonOperation.GreaterEqual:
                return ">=";
        }

        return "";
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        float m_offset = 0f;
        
        for(int i = 0; i < property.FindPropertyRelative("foldouts").arraySize; i++)
        {
            m_offset += property.FindPropertyRelative("foldouts").GetArrayElementAtIndex(i).boolValue ? 88f : 22f;
        }

        return m_offset + 22f * property.FindPropertyRelative("logic").arraySize + 22f + paddingBottom + paddingTop + 22f;
    }
}
