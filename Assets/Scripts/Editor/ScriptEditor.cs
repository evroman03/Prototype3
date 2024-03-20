using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

[CustomEditor(typeof(Tile))]
public class ScriptEditor : Editor
{
    
    public override void OnInspectorGUI()
    {
        Tile tile = (Tile)target;
        DrawDefaultInspector();
        switch (tile.type)
        {
            case Tile.TileType.Island:
                EditorGUILayout.LabelField("You selected Option 1!");
                break;
            case Tile.TileType.PirateCove:
                EditorGUILayout.LabelField("You selected Option 2!");
                break;
            case Tile.TileType.Ocean:
                EditorGUILayout.LabelField("You selected Option 3!");
                break;
            case Tile.TileType.RoyalPort:
                EditorGUILayout.LabelField("You selected Option 3!");
                break;
            case Tile.TileType.Border:
                EditorGUILayout.LabelField("You selected Option 3!");
                break;
        }
    }
    
}
