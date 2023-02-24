using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(BoardData), false)]
[CanEditMultipleObjects]
[System.Serializable]
public class BoardDataDrawer : Editor
{
    private BoardData BoardDataInstance => target as BoardData;

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        ClearBoardButton();
        EditorGUILayout.Space();

        DrawColumnInputFields();
        EditorGUILayout.Space();
        if (BoardDataInstance.board != null && BoardDataInstance.columns > 0 && BoardDataInstance.rows > 0)
        {
            DrawBoardTable();
        }
        serializedObject.ApplyModifiedProperties();

        if (GUI.changed)
        {
            EditorUtility.SetDirty(BoardDataInstance);
        }
        base.OnInspectorGUI();
    }

    private void ClearBoardButton()
    {
        if (GUILayout.Button("Clear Board"))
        {
            BoardDataInstance.Clear();
        }
    }

    private void DrawColumnInputFields()
    {
        var columnsTemp = BoardDataInstance.columns;
        var rowsTemp = BoardDataInstance.rows;

        BoardDataInstance.columns = EditorGUILayout.IntField("Columns", BoardDataInstance.columns);
        BoardDataInstance.rows = EditorGUILayout.IntField("Rows", BoardDataInstance.rows);

        if ((BoardDataInstance.columns != columnsTemp || BoardDataInstance.rows != rowsTemp) && BoardDataInstance.columns > 0 && BoardDataInstance.rows > 0)
        {
            BoardDataInstance.CreateNewBoard();
        }

    }
    private void DrawBoardTable()
    {
        var tableStyle = new GUIStyle("box");
        tableStyle.padding = new RectOffset(10, 10, 10, 10);
        tableStyle.margin.left = 32;

        var headerColumnStyle = new GUIStyle();
        headerColumnStyle.fixedWidth = 65;
        headerColumnStyle.alignment = TextAnchor.MiddleCenter;

        var rowStyle = new GUIStyle();
        rowStyle.fixedHeight = 25;
        rowStyle.alignment = TextAnchor.MiddleCenter;

        var dataFieldStyle = new GUIStyle(EditorStyles.miniButtonMid);
        /*dataFieldStyle.normal.background = Texture2D.grayTexture;
        dataFieldStyle.onNormal.background = Texture2D.whiteTexture;*/

        for (var row = BoardDataInstance.rows - 1; row >= 0; row--)
        {
            EditorGUILayout.BeginHorizontal(headerColumnStyle);

            for (var column = 0; column < BoardDataInstance.columns; column++)
            {
                dataFieldStyle.normal.background = ((row > 2 && row < 6) || (column > 2 && column < 6)) && !(row > 2 && row < 6 && column > 2 && column < 6)
                    ? Texture2D.blackTexture : Texture2D.grayTexture;

                EditorGUILayout.BeginHorizontal(rowStyle);
                var data = EditorGUILayout.TextArea(BoardDataInstance.board[row].column[column].ToString(), dataFieldStyle);
                BoardDataInstance.board[row].column[column] = int.Parse(data);
                EditorGUILayout.EndHorizontal();
            }
            EditorGUILayout.EndHorizontal();
        }



    }


}
