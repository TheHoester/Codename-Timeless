using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(RoomEditor))]
public class RoomEditorInspector : Editor
{
    private RoomEditor roomEditor;
    private Transform roomEditorTrans;

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        if (GUILayout.Button("Paint"))
            roomEditor.SwitchIsPainting();
        if (GUILayout.Button("Add Border"))
            roomEditor.CreateRoomBorder();
        if (GUILayout.Button("Create Navigation Nodes"))
            roomEditor.GenerateNavigationNodes();
        if (GUILayout.Button("Test Path"))
            roomEditor.TestNavigation();
        if (GUILayout.Button("Reset"))
            roomEditor.ResetTilemap();
    }

    public void OnSceneGUI()
    {
        if (roomEditor.IsPainting)
        {
            if (Event.current.type == EventType.mouseDown && Event.current.button == 0)
                roomEditor.CreateRoomTile(HandleUtility.GUIPointToWorldRay(Event.current.mousePosition).origin);

            Selection.activeGameObject = ((RoomEditor)target).gameObject;
        }
    }

    private void OnEnable()
    {
        roomEditor = (RoomEditor)target;
        roomEditorTrans = roomEditor.transform;
    }
}
