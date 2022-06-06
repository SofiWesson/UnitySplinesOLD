using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SplineScrObj))]
public class SplineScrObjButtons : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        SplineScrObj spliner = (SplineScrObj)target;

        if (GUILayout.Button("Add"))
        {
            spliner.Add();
        }
    }
}
