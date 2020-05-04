using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(Block))]
public class BlockEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        var block = target as Block;
        if (GUILayout.Button("Apply Texture")) block.ApplyTexture();
    }
}