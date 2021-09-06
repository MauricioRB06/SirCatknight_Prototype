using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SelectionRotateEditor : EditorWindow
{
    float angle = 0f;
    float maxAngle = 0f;
    float minAngle = 360f;
    // Start is called before the first frame update
    [MenuItem("Window/Rotate Objects")]
    public static void ShowWindow()
    {
        SelectionRotateEditor window = GetWindow<SelectionRotateEditor>("Rotate Objects");
        window.Init();
    }

    private void Init()
    {

    }

    private void OnGUI()
    {
        angle = EditorGUILayout.FloatField("Angle:", angle);
        if (GUILayout.Button("Rotate"))
        {
            GameObject[] goArray = Selection.gameObjects;
            foreach (GameObject go in goArray )
            {
                go.transform.rotation = Quaternion.Euler(0f, 0f, angle); 
            }
        }
        minAngle = EditorGUILayout.FloatField("Min Rand Angle:", minAngle);
        maxAngle = EditorGUILayout.FloatField("Max Rand Angle:", maxAngle);
        if (GUILayout.Button("Rotate Random Range"))
        {
            GameObject[] goArray = Selection.gameObjects;
            foreach (GameObject go in goArray)
            {
                go.transform.rotation = Quaternion.Euler(0f, 0f, Random.Range(minAngle,maxAngle));
            }
        }
    }

}
