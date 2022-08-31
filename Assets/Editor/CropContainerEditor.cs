using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CropsContainer))]
public class CropContainerEditor : Editor
{

    public override void OnInspectorGUI()
    {
        CropsContainer container = target as CropsContainer;

        if(GUILayout.Button("Clear container"))
        {
            for(int i = 0; i < container.crops.Count; i++)
            {
                container.Clear(container.crops[i]);
            }
        }
        DrawDefaultInspector();
    }
}
