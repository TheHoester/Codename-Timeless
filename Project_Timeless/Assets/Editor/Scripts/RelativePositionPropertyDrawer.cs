using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(RelativePosition))]
public class RelativePositionPropertyDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    { 
        EditorGUI.BeginProperty(position, label, property);

        position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

        var indent = EditorGUI.indentLevel;
        EditorGUI.indentLevel = 0;

        Rect xLabelRect = new Rect(position.x, position.y, 15, position.height);
        Rect xFieldRect = new Rect(position.x + 20, position.y, 30, position.height);
        Rect yLabelRect = new Rect(position.x + 60, position.y, 15, position.height);
        Rect yFieldRect = new Rect(position.x + 80, position.y, 30, position.height);

        EditorGUI.LabelField(xLabelRect, "X");
        EditorGUI.LabelField(yLabelRect, "Y");

        EditorGUI.PropertyField(xFieldRect, property.FindPropertyRelative("x"), GUIContent.none);
        EditorGUI.PropertyField(yFieldRect, property.FindPropertyRelative("y"), GUIContent.none);

        EditorGUI.indentLevel = indent;

        EditorGUI.EndProperty();
    }
}