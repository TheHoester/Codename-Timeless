using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TilesetsTool : EditorWindow
{
    string myString = "Hello World";
    bool groupEnabled;
    bool myBool = true;
    float myFloat = 1.23f;

    private int selectedIndex = -1;
    private Color color_selected = Color.blue;

    Vector2 scrollPos;
    string t = "This is a string inside a Scroll view!";

    [MenuItem("TheHoesterTools/Tilesets")]
    public static void ShowWindow()
    {
        GetWindow(typeof(TilesetsTool));
    }

    private void OnGUI()
    {
        /*
        GUILayout.Label("Base Settings", EditorStyles.boldLabel);
        myString = EditorGUILayout.TextField("Text Field", myString);

        groupEnabled = EditorGUILayout.BeginToggleGroup("Optional Settings", groupEnabled);
        myBool = EditorGUILayout.Toggle("Toggle", myBool);
        myFloat = EditorGUILayout.Slider("Slider", myFloat, -3, 3);
        EditorGUILayout.EndToggleGroup();
        if (GUILayout.Button("Button"))
        {
            myBool = true;
            myString = "You pushed it didn't you";
        }

        Color color_default = GUI.backgroundColor;
        List<string> p_list = new List<string>();
        p_list.Add("This");
        p_list.Add("Is");
        p_list.Add("A");
        p_list.Add("List");
        p_list.Add("Of");
        p_list.Add("Strings");

        for (int i = 0; i < p_list.Count; i++)
        {
            GUI.backgroundColor = (selectedIndex == i) ? color_selected : color_default;

            EditorGUILayout.LabelField(p_list[i]);

            Rect lastRect = GUILayoutUtility.GetLastRect();
            if (GUI.Button(lastRect, p_list[i]))
            {
                selectedIndex = i;
            }
        }
        */

        scrollPos = EditorGUILayout.BeginScrollView(scrollPos, false, true, GUILayout.Width(position.width), GUILayout.Height(position.height / 2));

        Color color_default = GUI.backgroundColor;

        for (int i = 0; i < 10; i++)
        {
            GUI.backgroundColor = (selectedIndex == i) ? color_selected : color_default;
            
            if (GUILayout.Button("Button " + i, GUILayout.Width(position.width - 20)))
                selectedIndex = i;
        }

        EditorGUILayout.EndScrollView();

        /*
        if (GUILayout.Button("Add More Text", GUILayout.Width(100), GUILayout.Height(100)))
            t += " \nAnd this is more text!";
        EditorGUILayout.EndHorizontal();
        if (GUILayout.Button("Clear"))
            t = "";
        */
    }
}
