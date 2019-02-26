using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(RoadManager))]
public class RoadManagerInspector : Editor
{
    private RoadManager manager;
    private Transform managerTransform;

    private List<HandleData> handleData;

    private int selectedHandleID = -1;
    private Vector2 selectionMouseOrigin;

    private bool hasUpdated = false;

    private void OnEnable()
    {
        manager = (RoadManager)target;
        managerTransform = manager.transform;

        if (handleData == null || handleData.Count == 0)
        {
            List<HandleData> managerData = manager.GetHandlePositions();
            handleData = new List<HandleData>();
            foreach (HandleData data in managerData)
                handleData.Add(new HandleData(GUIUtility.GetControlID(FocusType.Passive), data.position, data.direction));
        }

        Tools.hidden = true;
        hasUpdated = true;
    }

    private void OnDisable()
    {
        Tools.hidden = false;
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        if (GUILayout.Button("Reset"))
        {
            manager.ResetTilemap();
            hasUpdated = true;
        }

        if (GUILayout.Button("Load Prefabs"))
            manager.LoadAssets();

        if (GUILayout.Button("Undo"))
        {
            manager.UndoLastAction();
            hasUpdated = true;
        }
    }

    public void OnSceneGUI()
    {
        if (hasUpdated)
        {
            List<HandleData> managerData = manager.GetHandlePositions();
            handleData = new List<HandleData>();
            foreach(HandleData data in managerData)
                handleData.Add(new HandleData(GUIUtility.GetControlID(FocusType.Passive), data.position, data.direction));

            hasUpdated = false;
        }

        if (Event.current.type == EventType.Repaint && handleData != null)
            foreach (HandleData data in handleData)
                DrawArrow(data);
        else if (Event.current.type == EventType.Layout && handleData != null)
        {
            foreach (HandleData data in handleData)
                LayoutArrow(data);

            HandleUtility.Repaint();
        }
        else if (Event.current.type == EventType.mouseDown && Event.current.button == 0)
        {
            selectedHandleID = HandleUtility.nearestControl;
            selectionMouseOrigin = Event.current.mousePosition;
            HandleUtility.Repaint();
        }
        else if (Event.current.type == EventType.mouseUp && Event.current.button == 0)
        {
            if (selectedHandleID != -1)
            {
                HandleData tempData = new HandleData(-1, Vector3.zero, Direction.None);
                foreach (HandleData data in handleData)
                {
                    if (data.id == selectedHandleID)
                    {
                        tempData = data;
                        break;
                    }
                }

                if (tempData.direction != Direction.None)
                {
                    Vector3 direction = HelperFunctions.DirectionToVector3(tempData.direction);
                    Vector3 position = tempData.position + (direction * Constants.TILECENTREOFFSET);
                    float distance = HandleUtility.CalcLineTranslation(selectionMouseOrigin, Event.current.mousePosition, position, direction);
                    int tiles = Mathf.FloorToInt(distance / Constants.TILESIZE);

                    if (tiles > 1)
                    {
                        manager.CreateNewTiles(tempData.direction, tiles, HelperFunctions.Vector3ToRelativePosition(tempData.position));
                        hasUpdated = true;
                    }
                }
            }

            selectedHandleID = -1;
            HandleUtility.Repaint();
        }
    }

    #region Arrow Handler Methods

    /// <summary>
    /// Draws an arrow handle at a point and in the direction specified, displaying it at mouse distance away if being dragged.
    /// </summary>
    /// <param name="data">The data for the arrow handle, holding its position, direction and ID.</param>
    private void DrawArrow(HandleData data)
    {
        DrawArrow(data.position, HelperFunctions.DirectionToVector3(data.direction), HelperFunctions.DirectionToColour(data.direction), data.id);
    }

    /// <summary>
    /// Draws an arrow handle at a point and in the direction specified, displaying it at mouse distance away if being dragged.
    /// </summary>
    /// <param name="origin">Centre point (position) of the tile that it is positioned to.</param>
    /// <param name="vDirection">Direction the arrow will be facing.</param>
    /// <param name="colour">Colour of the arrow.</param>
    /// <param name="id">The control ID of the arrow.</param>
    private void DrawArrow(Vector3 origin, Vector3 vDirection, Color colour, int id)
    {
        Vector3 position = origin + (vDirection * Constants.TILECENTREOFFSET);

        if (selectedHandleID == id)
        {
            // Handle is currently selected and being dragged.
            Handles.color = Color.magenta;

            float distance = HandleUtility.CalcLineTranslation(selectionMouseOrigin, Event.current.mousePosition, position, vDirection);
            int tiles = Mathf.FloorToInt(distance / Constants.TILESIZE);

            // Draws lines to show tiles that will be created.
            if (tiles > 0)
            {
                Vector3 left = position + (Vector3.Cross(vDirection, Vector3.down).normalized * Constants.TILECENTREOFFSET);
                Handles.DrawLine(left, left + (vDirection * Constants.TILESIZE * tiles));
            }

            position += vDirection * distance;
        }
        else
            Handles.color = colour;

        // Redraws the handle at the mouse position distance from its starting place.
        Handles.ArrowHandleCap(id, position, Quaternion.LookRotation(vDirection), HandleUtility.GetHandleSize(position), EventType.Repaint);
    }

    /// <summary>
    /// Handles the arrow handle when the layout event is triggered.
    /// </summary>
    /// <param name="data">The data for the arrow handle, holding its position, direction and ID.</param>
    private void LayoutArrow(HandleData data)
    {
        LayoutArrow(data.position, HelperFunctions.DirectionToVector3(data.direction), data.id);
    }

    /// <summary>
    /// Handles the arrow handle when the layout event is triggered.
    /// </summary>
    /// <param name="origin">Centre point (position) of the tile that it is positioned to.</param>
    /// <param name="direction">Direction the arrow will be facing.</param>
    /// <param name="id">The control ID of the arrow.</param>
    private void LayoutArrow(Vector3 origin, Vector3 direction, int id)
    {
        Vector3 position = origin + (direction * Constants.TILECENTREOFFSET);
        Handles.ArrowHandleCap(id, position, Quaternion.LookRotation(direction), HandleUtility.GetHandleSize(position), EventType.Layout);
    }

    #endregion

}
